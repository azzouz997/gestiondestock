using ElamanaTakaful.Application.Common;
using ElamanaTakaful.Application.Common.Repositories;
using ElamanaTakaful.Application.Services.FunctionServices;
using ElamanaTakaful.Application.Services.OrderServices;
using ElamanaTakaful.Application.Services.ProductServices;
using ElamanaTakaful.Application.Services.PropositionServices;
using ElamanaTakaful.Application.Services.RoleServices;
using ElamanaTakaful.Application.Services.SupplierServices;
using ElamanaTakaful.Application.Services.UserServices;
using ElamanaTakaful.Domain.Entities;
using ElamanaTakaful.Infrastructure.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEntityFrameworkSqlServer();

            services.AddDbContext<ElamanaTakafulContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ElamanaTakafulDatabase")));

            services.AddScoped<IDataRepository<User>, UserService>();
            services.AddScoped<IDataRepository<Role>, RoleService>();
            services.AddScoped<IDataRepository<Function>, FunctionService>();
            services.AddScoped<IDataRepository<Product>, ProductService>();
            services.AddScoped<IDataRepository<Supplier>, SupplierService>();
            services.AddScoped<IDataRepository<Proposition>, PropositionService>();
            services.AddScoped<IDataRepository<Order>, OrderService>();
            services.AddScoped<ProductRepository>();

            var key = Configuration["JWT:Secret"];
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x=> 
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));

            services.AddSingleton<IMongoClient>(c =>
            {
                var login = "mongoadmin";
                var password = Uri.EscapeDataString("ElamanaTakaful");
                var server = "161.97.173.185:27017";

                return new MongoClient(
                    string.Format("mongodb+srv://{0}:{1}@{2}/ElamanaTakafulBD?retryWrites=true&w=majority", login, password, server));
            });

            services.AddScoped(c =>
                c.GetService<IMongoClient>().StartSession());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
