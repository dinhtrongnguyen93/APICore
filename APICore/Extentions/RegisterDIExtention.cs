using ApiCore.Models.DAL.IService;
using ApiCore.Models.DAL.Service;
using ApiCore.Models.Database.Repository.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace WebCoreMVC.Extentions
{
    public static class RegisterDIExtention
    {
        public static void RegisterServiceDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<UnitOfWorkOption>((UnitOfWorkOption option)=> {
                option.DataBaseConnectString = configuration.GetConnectionString("DefaultConnection");
            });
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>();

        }
    }
}
