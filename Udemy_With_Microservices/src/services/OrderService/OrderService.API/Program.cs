using Autofac.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.CQRS.Queries.GetOrderByUserId;
using OrderService.Infrastructure.Context;
using ServicesShared.Services;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var RequiredPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityReturnUrl"];
    options.Audience = "resource_order";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(RequiredPolicy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), configure =>
    {
        configure.MigrationsAssembly("OrderService.Infrastructure");
    });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetOrderByUseIdQueryHandler>());
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService , SharedIdentityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
