using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Munizoft.Azure.Services;
using Munizoft.Azure.Services.Mappings;
using Munizoft.Azure.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munizoft.Azure.BlogService.WebAPI
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
            services.Configure<AzureBlobServicesOptions>(options =>
            {
                options.Key = Configuration["AzureBlog:Key"];
                options.ConnectionString = Configuration["AzureBlog:ConnectionString"];
            });

            services.AddSingleton<BlobServiceClient>(x => new BlobServiceClient(Configuration["AzureBlog:ConnectionString"]));

            services.AddSingleton<IBlobService, BlobService>();

            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddAutoMapper(typeof(AzureMapping));
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
