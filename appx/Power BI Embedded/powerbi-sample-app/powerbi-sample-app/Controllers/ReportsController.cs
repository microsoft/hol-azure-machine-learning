using Microsoft.PowerBI.Api.Beta;
using Microsoft.PowerBI.Security;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace paas_demo.Controllers
{
    public class ReportsController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> GetReports()
        {
            Guid workspaceId = Guid.Parse(ConfigurationManager.AppSettings["powerbi:WorkspaceId"]);
            var devToken = PowerBIToken.CreateDevToken(ConfigurationManager.AppSettings["powerbi:WorkspaceCollection"], workspaceId);
            using (var client = this.CreatePowerBIClient(devToken))
            {
                var reportsResponse = await client.Reports.GetReportsAsync();
                return Ok(reportsResponse.Value
                    .Select(report => new
                    {
                        workspaceId = workspaceId,
                        reportId = report.Id,
                        reportName = report.Name,
                        reportEmbedUrl = report.EmbedUrl,
                    }));
            }
        }

        private IPowerBIClient CreatePowerBIClient(PowerBIToken token)
        {
            var jwt = token.Generate(ConfigurationManager.AppSettings["powerbi:SigningKey"]);
            var credentials = new TokenCredentials(jwt, "AppToken");
            var client = new PowerBIClient(credentials)
            {
                BaseUri = new Uri(ConfigurationManager.AppSettings["powerbi:ApiUrl"])
            };

            return client;
        }
    }
}