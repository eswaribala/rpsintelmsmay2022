using CQRSDemo.Commands;
using CQRSDemo.Events;
using CQRSDemo.Models.Mongo;
using CQRSDemo.Models.Sqlite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSDemo
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRSDemo", Version = "v1" });
            });
            services.AddDbContext<CustomerSQLiteDatabaseContext>
               (options => options.UseSqlite
               (Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<CustomerSQLiteRepository>();
            services.AddTransient<CustomerMongoRepository>();
            services.AddTransient<AMQPEventPublisher>();
            services.AddSingleton<CustomerMessageListener>();
            services.AddScoped<ICommandHandler<Command>, CustomerCommandHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService
               <IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.
                    GetRequiredService<CustomerSQLiteDatabaseContext>();
                context.Database.EnsureCreated();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRSDemo v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
