using ClientForWeb.Abstractions;
using ClientForWeb.Configurations;
using ClientForWeb.Handlers;
using ClientForWeb.Services;

namespace ClientForWeb.Extensions
{
    public static class ServiceExtension
    {
        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration Configuration)
        {
            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
         

            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.BaseUrl);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();


        }
        }
    }
