using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AVIVA.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            _ = services.AddSwaggerGen(c =>
            {
                Uri uri = new("https://janedoe.com/contact");
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Clean Architecture - AVIVA API",
                    Description = "This Api will be responsible for overall data distribution and authorization.",
                    Contact = new OpenApiContact
                    {
                        Name = "AVIVA",
                        Email = "aviva@aviva.com",
                        Url = uri,
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
                // c.OperationFilter<HeaderTenant>();
            });
        }

        public static void AddControllersExtension(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                })
                ;
        }

        //Configure CORS to allow any origin, header and method.
        //Change the CORS policy based on your requirements.
        //More info see: https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-3.0

        public static void AddCorsExtension(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }

        public static void AddVersionedApiExplorerExtension(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });
        }

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
        }

        public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCognitoIdentity();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = configuration["Cognito:Authority"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        RoleClaimType = "cognito:groups"
                    };
                });
        }

        public static void AddAuthorizationPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            string admin = configuration["ApiRoles:AdminRole"],
                    owner = configuration["ApiRoles:OwnerRole"],
                    clinician = configuration["ApiRoles:ClinicianRole"],
                    support = configuration["ApiRoles:SupportRole"];

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationConstants.SupportPolicy, policy => policy.RequireAssertion(context => context.User.HasClaim(c => c.Type == "cognito:groups" && c.Value == support)));
                options.AddPolicy(AuthorizationConstants.AdminPolicy, policy => policy.RequireRole(admin));
                options.AddPolicy(AuthorizationConstants.OwnerPolicy, policy => policy.RequireRole(owner, admin));
                options.AddPolicy(AuthorizationConstants.ClinicianPolicy, policy => policy.RequireRole(clinician, owner, admin));
            });
        }
    }
}