
using API.ATM.Application;
using API.ATM.Application.Contracts;
using API.ATM.Application.Handlers;
using API.ATM.Domain.Options;
using API.ATM.Infrastructure;
using API.ATM.Infrastructure.Repositories.Commands;
using API.ATM.Infrastructure.Services;
using API.ATM.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace API.ATM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Builder = WebApplication.CreateBuilder(args);
            var Configuration = Builder.Configuration;
            var Services = Builder.Services;

            Services.Configure<AtmLimitsOptions>(Configuration.GetSection("AtmLimits"));

            Services.AddControllers(options => {})
            .ConfigureApiBehaviorOptions(options => 
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();

            Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API.ATM", Version = "v1" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Ingrese el token JWT como: {token}, la palabra Bearer ya es automatica"
                };

                options.AddSecurityDefinition("Bearer", securityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var key = Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]!);
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(o =>
               {
                   o.TokenValidationParameters = new()
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = Configuration["Jwt:Issuer"],
                       ValidAudience = Configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(key)
                   };
                   o.Events = new JwtBearerEvents
                   {
                       OnChallenge = ctx =>
                       {
                           ctx.HandleResponse();
                           ctx.Response.StatusCode = 200;
                           return ctx.Response.WriteAsJsonAsync(ApiEnvelope.Fail(ErrorCodes.InvalidCredentials, "Token required"));
                       }
                   };
               });
            Services.AddAuthorization();

            Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            Services.AddScoped<IAtmRepository, AtmRepository>();
            Services.AddScoped<ILoginRepository, LoginRepository>();

            Services.AddScoped<WithdrawCommandExecutor>();
            Services.AddScoped<DepositCommandExecutor>();
            Services.AddScoped<ChangePinCommandExecutor>();
            Services.AddScoped<BalanceQueryExecutor>();

            Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly);
            });

            var app = Builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}

