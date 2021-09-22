using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using teamcare.data.Data;
using Microsoft.IdentityModel.Tokens;
using teamcare.web.app.Middleware;
using teamcare.common.Configuration;
using SixLabors.ImageSharp.Web.Providers.Azure;
using SixLabors.ImageSharp.Web.DependencyInjection;
using SixLabors.ImageSharp.Web.Caching;
using SixLabors.ImageSharp.Web.Processors;
using teamcare.business.Services;
using teamcare.data.Entities;

namespace teamcare.web.app
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<AzureStorageSettings>().Bind(Configuration.GetSection("AzureStorageSettings"));
            services.AddControllersWithViews().AddNewtonsoftJson(x =>
 x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.AddImageSharp(options =>
            {
                options.BrowserMaxAge = TimeSpan.FromDays(7);
                options.CacheMaxAge = TimeSpan.FromDays(30);
            }).ClearProviders().Configure<AzureBlobStorageImageProviderOptions>(options =>
            {
                // The "BlobContainers" collection allows registration of multiple containers.
                options.BlobContainers.Add(new AzureBlobContainerClientOptions
                {
                    ConnectionString = Configuration.GetSection("AzureStorageSettings").GetValue<string>("ConnectionString"),
                    ContainerName = Configuration.GetSection("AzureStorageSettings").GetValue<string>("Container")
                });
            }).SetCache<PhysicalFileSystemCache>()
            .SetCacheHash<CacheHash>()
            .AddProvider<AzureBlobStorageImageProvider>()
            .AddProcessor<ResizeWebProcessor>()
            .AddProcessor<FormatWebProcessor>()
            .AddProcessor<BackgroundColorWebProcessor>();
            services.RegisterServices();
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                auth.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(o =>
                {
                    o.Cookie.SameSite = SameSiteMode.None;
                })
                .AddOpenIdConnect(opts =>
                {
                    Configuration.GetSection("AzureAd").Bind(opts);
                    opts.SaveTokens = true;
                    opts.TokenValidationParameters.IssuerValidator = ValidateIssuerWithPlaceholder;
                    opts.Events = new OpenIdConnectEvents
                    {
                        OnAuthorizationCodeReceived = async ctx =>
                        {
                        },
                        OnTicketReceived = async ctx =>
                        {
                        },
                        OnTokenValidated = async ctx =>
                        {
                            var userService = ctx.HttpContext.RequestServices.GetService<IUserService>();
                            var allUsers = await userService.ListAllAsync();
                            var loggedInEmail = ctx.Principal.Claims.FirstOrDefault(i =>
                                i.Type.Equals(common.ReferenceData.ClaimTypes.PreferredUsername,
                                    StringComparison.OrdinalIgnoreCase))?.Value;


                            if (string.IsNullOrWhiteSpace(loggedInEmail))
                            {
                                // throw error here
                            }
                            else
                            {
                                var currentUser = allUsers.FirstOrDefault(i =>
                                    i.IsActive && i.Email != null && i.Email.Equals(loggedInEmail, StringComparison.OrdinalIgnoreCase));
                                if (currentUser != null)
                                {
                                    var _auditService = ctx.HttpContext.RequestServices.GetService<IAuditService>();
                                    _auditService.Execute(async repository =>
                                    {
                                        await repository.CreateAuditRecord(new Audit { Action = "Sign In", Details = (currentUser.FirstName + " " + currentUser.LastName) + " has signed in.", UserReference = "", CreatedBy = currentUser.Id });
                                    });
                                    var claims = new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Role, currentUser.UserRole.ToString()),
                                        new Claim(common.ReferenceData.ClaimTypes.UserId, currentUser.Id.ToString()),
                                        new Claim(common.ReferenceData.ClaimTypes.UserEmail, currentUser.Email),
                                        new Claim(common.ReferenceData.ClaimTypes.UserPhoto, currentUser.ProfilePhoto.BlobName),
                                    };
                                    var appIdentity = new ClaimsIdentity(claims);

                                    ctx.Principal.AddIdentity(appIdentity);
                                }
                                else
                                {
                                    ctx.HandleResponse();
                                    ctx.Response.Redirect("/Authentication/SignOut");
                                }
                            }
                        }
                    };
                });

            services.AddMvc(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest);


            services.AddDbContext<TeamcareDbContext>(options =>
                   options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());

            services.AddAutoMapper(typeof(Startup));

            /*
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            */
            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseImageSharp();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static string ValidateIssuerWithPlaceholder(string issuer, SecurityToken token, TokenValidationParameters parameters)
        {
            // Accepts any issuer of the form "https://login.microsoftonline.com/{tenantid}/v2.0",
            // where tenantid is the tid from the token.

            if (token is JwtSecurityToken jwt)
            {
                if (jwt.Payload.TryGetValue("tid", out var value) &&
                    value is string tokenTenantId)
                {
                    var jwtValidIssuers = (parameters.ValidIssuers ?? Enumerable.Empty<string>())
                        .Append(parameters.ValidIssuer)
                        .Where(i => !string.IsNullOrEmpty(i));

                    if (jwtValidIssuers.Any(i => i.Replace("{tenantid}", tokenTenantId) == issuer))
                        return issuer;
                }
            }

            // Recreate the exception that is thrown by default
            // when issuer validation fails
            var validIssuer = parameters.ValidIssuer ?? "null";
            var validIssuers = parameters.ValidIssuers == null
                ? "null"
                : !parameters.ValidIssuers.Any()
                    ? "empty"
                    : string.Join(", ", parameters.ValidIssuers);
            string errorMessage = FormattableString.Invariant(
                $"IDX10205: Issuer validation failed. Issuer: '{issuer}'. Did not match: validationParameters.ValidIssuer: '{validIssuer}' or validationParameters.ValidIssuers: '{validIssuers}'.");

            throw new SecurityTokenInvalidIssuerException(errorMessage)
            {
                InvalidIssuer = issuer
            };
        }
    }
}

