using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Web.Framework
{
    public abstract class BaseStartup
    {
        protected BaseStartup(IConfiguration configuration, string swaggerUrl, Info swaggerInfo)
        {
            Configuration = configuration;
            SwaggerUrl = swaggerUrl;
            SwaggerInfo = swaggerInfo;
        }

        public IConfiguration Configuration { get; }

        public string SwaggerUrl { get; }

        public Info SwaggerInfo { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc(SwaggerUrl, SwaggerInfo);
                opt.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
            services.AddCors();
            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // var section = Configuration.GetSection("ConfTest");

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
            });

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
