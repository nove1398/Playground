using LdapForNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

//using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using static LdapForNet.Native.Native;

namespace LDAP.Controllers
{
    [ApiController]
    [Route("ldap")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        public class ADUser
        {
            public string LoginName { get; set; }
            public string DisplayName { get; set; }
            public string Created { get; set; }
            public DateTime Updated { get; set; }
            public string EmailAddress { get; set; }
            public string[] Groups { get; set; }
            public string Mobile { get; set; }
            public string Extension { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Department { get; set; }
        }

        [HttpGet("test")]
        public async Task<IActionResult> Get2()
        {
            using var cn = new LdapConnection();

            // connect
            cn.Connect("ldap://dc1.mtw.gov.jm", LdapVersion.LDAP_VERSION3);
            // bind using kerberos credential cache file
            cn.Bind(userDn: "efranklin", password: "Jerome1398@");
            // call ldap op
            //var entries = cn.Search("dc=efranklin,dc=com", "(objectClass=*)");
            var me = await cn.WhoAmI();
            var data = await cn.SearchAsync("DC=mtw,DC=gov,DC=jm",
                "((sAMAccountName=efr*))");
            /*new string[] {
                    "whenChanged",
                    "memberOf",
                    "whenCreated",
                    "mail",
                    "userPrincipalName",
                    "mobile",
                    "homePhone",
                    "ipPhone",
                    "displayName",
                    "sAMAccountName",
                    "department",
                    "description",
                    "lastLogonTimestamp",}*/
            List<ADUser> infos = new();
            foreach (var item in data)
            {
                foreach (var pair in item.Attributes
                                        .Select(_ => new { _.Key, Value = _.Value.FirstOrDefault() }))
                {
                    infos.Add(new ADUser
                    {
                        /*
                         Updated = user.GetWhenChanged().Value,
                         Groups = user.GetMemberOf().ToArray(),
                         DisplayName = user.GetAttribute("displayName").GetValue<List<string>>()[0],
                         //Created = user.GetAttribute("whenCreated").GetValue<List<string>>().FirstOrDefault(),
                           EmailAddress = user.GetAttribute("mail").GetValue<List<string>>().FirstOrDefault() ??
                                       user.GetAttribute("userPrincipalName").GetValue<List<string>>().FirstOrDefault() ??
                                       "N/A",
                           LoginName = user.GetAttribute("sAMAccountName").GetValue<List<string>>().FirstOrDefault(),
                           Mobile = user.GetAttribute("mobile").GetValue<List<string>>().FirstOrDefault() ??
                                       user.GetAttribute("homePhone").GetValue<List<string>>().FirstOrDefault() ??
                                       "N/A",
                           Extension = user.GetAttribute("ipPhone").GetValue<List<string>>().FirstOrDefault(),
                           Department = user.GetAttribute("department").GetValue<List<string>>().FirstOrDefault(),
                           Description = user.GetAttribute("description").GetValue<List<string>>().FirstOrDefault(),
                           Title = user.GetAttribute("title").GetValue<List<string>>().FirstOrDefault(),*/
                    });
                }
            }
            return Ok(data);
        }
    }
}