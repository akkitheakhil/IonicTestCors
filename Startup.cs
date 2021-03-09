using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IonicTest
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
            services.AddSwaggerGen();
            // app settings configuration.
           /* services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                *//*options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    var corsUrlSection = Configuration.GetSection("AllowedOrigins");
                    var corsUrls = corsUrlSection.Get<string[]>();
                    policy.WithOrigins(corsUrls)
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination") // add any customer header if we are planning to send any 
                        .AllowAnyMethod();
                });*//*
            

            });*/
      /*      services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("capacitor://account.hrblock.com", "https://www.account.hrblock.com"));
            });*/
            services.AddCors(options => options.AddDefaultPolicy(
           builder => builder.WithOrigins("https://www.account.hrblock.com"))
       );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ionic test API V1"); });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            /*app.UseCors(builder => builder.WithOrigins("https://localhost:44306")
                              .AllowAnyMethod()
                              .WithHeaders("authorization", "accept", "content-type", "AllowSpecificOrigin"));*/


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
