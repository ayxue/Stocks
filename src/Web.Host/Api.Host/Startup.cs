//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Swashbuckle.AspNetCore.Swagger;

//namespace Api.Host
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

//            services.AddSwaggerGen(opt =>
//            {

//                opt.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

//                //var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
//                //var xmlPath = Path.Combine(basePath, "Api.Host.xml");
//                //opt.IncludeXmlComments(xmlPath);

//                opt.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
//            });
//            services.AddCors();
//            services.AddDistributedMemoryCache();
//            services.AddSession();
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseMvc();

//            app.UseSwagger();
//            app.UseSwaggerUI(c =>
//            {
//                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//                c.RoutePrefix = string.Empty;
//            });
//        }
//    }
//}
