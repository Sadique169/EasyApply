using EasyApply.Core;

namespace API
{
    public static class DIExtension
    {
        public static IServiceCollection AddDatabaseDependency(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();  
            return services;
        }

    }
}
