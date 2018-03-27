using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using EvasaoEscolar.MODELS;
using EvasaoEscolar.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using EvasaoEscolar.CONTEXTO;
using EvasaoEscolar.CONTRACTS;
using EvasaoEscolar.REPOSITORIES;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Cors;

namespace EvasaoEscolar
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

         public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy("AllowAnyOrigin", builder => {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });


            services.AddDbContext<EvasaEscolarContext>(options => 
                                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

                                //token
                                 var signingConfigurations = new SigningConfigurations();
                services.AddSingleton(signingConfigurations);

                var tokenConfigurations = new TokenConfigurations();
                new ConfigureFromConfigurationOptions<TokenConfigurations>(
                    Configuration.GetSection("TokenConfigurations"))
                        .Configure(tokenConfigurations);
                services.AddSingleton(tokenConfigurations);


                services.AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(bearerOptions =>
                {
                    var paramsValidation = bearerOptions.TokenValidationParameters;
                    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                    paramsValidation.ValidAudience = tokenConfigurations.Audience;
                    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                    // Valida a assinatura de um token recebido
                    paramsValidation.ValidateIssuerSigningKey = true;

                    // Verifica se um token recebido ainda é válido
                    paramsValidation.ValidateLifetime = true;

                    // Tempo de tolerância para a expiração de um token (utilizado
                    // caso haja problemas de sincronismo de horário entre diferentes
                    // computadores envolvidos no processo de comunicação)
                    paramsValidation.ClockSkew = TimeSpan.Zero;
                });

                // Ativa o uso do token como forma de autorizar o acesso
                // a recursos deste projeto
                services.AddAuthorization(auth =>
                {
                    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser().Build());
                });
                                // token

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            

           services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

          //  app.Run(async (context) =>
          //  {
               // await context.Response.WriteAsync("Hello World!");
           // });

             app.UseMvc();
             app.UseCors("AllowAnyOrigin");
        }
    }
}
