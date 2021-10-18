using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using teamcare.business.Services;
using teamcare.data.Data;
using teamcare.data.Entities;
using teamcare.data.Repositories;

namespace teamcare.web.app.Middleware
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(s =>
            {
                var contextAccessor = s.GetService<IHttpContextAccessor>();
                var user = contextAccessor?.HttpContext?.User;
                return user ?? throw new System.Exception("User not resolved");
            });

            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IServiceUserService, ServiceUserService>();
            services.AddScoped<IResidenceService, ResidenceService>();
            services.AddScoped<IDocumentUploadService, DocumentUploadService>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IFavouriteServiceUserService, FavouriteServiceUserService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IResidenceRepository, ResidenceRepository>();
            services.AddScoped<IFavouriteServiceUserRepository, FavouriteServiceUserRepository>();
            services.AddScoped<IResidenceRepository, ResidenceRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IServiceUserRepository, ServiceUserRepository>();
            services.AddScoped<IDocumentUploadRepository, DocumentUploadRepository>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IServiceUserLogService, ServiceUserLogService>();
            services.AddScoped<IServiceUserLogRepository, ServiceUserLogRepository>();
            services.AddScoped<ISkillGroupsService, SkillGroupsService>();
            services.AddScoped<ISkillGroupsRepository, SkillGroupsRepository>();
            services.AddScoped<ILivingSkillService,LivingSkillService>();
            services.AddScoped<ILivingSkillRepository, LivingSkillRepository>();
            services.AddScoped<IAssessmentService, AssessmentService>();
            services.AddScoped<IAssessmentRepository, AssessmentRepository>();
            services.AddScoped<IAssessmentSkillService, AssessmentSkillService>();
            services.AddScoped<IAssessmentSkillRepository, AssessmentSkillRepository>();
            services.AddScoped<IAssessmentTypeService, AssessmentTypeService>();
            services.AddScoped<IAssessmentTypeRepository, AssessmentTypeRepository>();            
            services.AddScoped<IServiceUserDocumentService, ServiceUserDocumentService>();
            services.AddScoped<IServiceUserDocumentRepository, ServiceUserDocumentRepository>();
            services.AddScoped<IHealthMedicationService, HealthMedicationService>();
            services.AddScoped<IHealthMedicationRepository, HealthMedicationRepository>();


            return services;
        }
    }
}
