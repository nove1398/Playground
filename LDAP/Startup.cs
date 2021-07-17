using LinqToLdap;
using LinqToLdap.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace LDAP
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
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
            });

            var config = new LdapConfiguration().MaxPageSizeIs(1000);
            //add mapping
            config.AddMapping(new AttributeClassMap<ADUser>());
            config.ConfigureFactory("ldap://dc1.mtw.gov.jm")
                    .AuthenticateBy(AuthType.Negotiate)
                    .AuthenticateAs(new NetworkCredential { Domain = "MTWDOMAIN", Password = "Jerome1398@", UserName = "efranklin" })
                    .ProtocolVersion(3)
                    .UsePort(389);
            config.UseStaticStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}