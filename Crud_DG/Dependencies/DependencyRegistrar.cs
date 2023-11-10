using Crud_DG.Services;

namespace FMA.Web.Dependencies
{
    public static class DependencyRegistrar
    {
        public static async Task<IServiceCollection> RegisterDependencies(this IServiceCollection services)
        {
            
            services.AddScoped<IEmployeeService, EmployeeService>();



            // Add more registrations as needed
            return services;
        }
    }
}
