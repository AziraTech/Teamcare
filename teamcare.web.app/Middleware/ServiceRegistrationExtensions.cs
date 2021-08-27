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
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IServiceUserService, ServiceUserService>();
            services.AddScoped<IResidenceService, ResidenceService>();
            services.AddScoped<IDocumentUploadService, DocumentUploadService>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IResidenceRepository, ResidenceRepository>();          
            services.AddScoped<IResidenceRepository, ResidenceRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IServiceUserRepository, ServiceUserRepository>();
            services.AddScoped<IDocumentUploadRepository, DocumentUploadRepository>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }
    }
}
