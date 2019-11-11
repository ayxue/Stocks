using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Web.Framework;

namespace OApiReporting.Apis.Configurations
{
    public class Startup : BaseStartup
    {
        public Startup(IConfiguration configuration) : base(configuration, "v1", new Info { Title = "My Title" })
        {
        }

    }
}
