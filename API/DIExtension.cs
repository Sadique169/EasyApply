using EasyApply.Core;
using EasyApply.Core.IRepositories;
using EasyApply.Core.Repositories;

namespace API
{
    public static class DIExtension
    {
        public static IServiceCollection AddDatabaseDependency(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            return services;
        }

    }
}
