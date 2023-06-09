﻿using ClientForWeb.Abstractions;
using ClientForWeb.Configurations;
using ClientForWeb.Handlers;
using ClientForWeb.Services;
using IdentityModel.AspNetCore.AccessTokenManagement;
using ServicesShared.Services;

namespace ClientForWeb.Extensions
{
    public static class ServiceExtension
    {
       
        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAccessTokenManagement();
            

            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
            services.AddHttpClient<IIdentityService, IdentityService>();
         

            services.AddScoped<ClientCreadentialTokenHandler>();
            services.AddScoped<IClientCredentialsTokenService ,ClientCredentialsTokenService>();
          
         

            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.BaseUrl);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<ICatalogService, CatalogService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings?.GatewayUrl + serviceApiSettings?.CatalogPath);
            }).AddHttpMessageHandler<ClientCreadentialTokenHandler>();

            services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayUrl}{serviceApiSettings?.PhotoPath}");
            }).AddHttpMessageHandler<ClientCreadentialTokenHandler>();

            services.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayUrl}{serviceApiSettings.BasketPath}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IDiscountService, DiscountService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayUrl}{serviceApiSettings.DiscountPath}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IPaymentService, PaymentService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayUrl}{serviceApiSettings?.FakePaymentPath}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IOrderService, OrderService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayUrl}{serviceApiSettings?.FakePaymentPath}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();




        }
        }
    }
