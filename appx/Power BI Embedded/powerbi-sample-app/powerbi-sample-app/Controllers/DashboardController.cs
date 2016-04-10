using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.PowerBI.Api.Beta;
using Microsoft.PowerBI.Security;
using powerbi_sample_app.Models;
using powerbi_sample_app.Reporting;
using System.Collections.Generic;
using Microsoft.PowerBI.Api.Beta.Models;
using System.Web.Configuration;

namespace powerbi_sample_app.Controllers
{
    public class DashboardController : Controller
    {
        private string workspaceCollection;
        private ReportsViewModel workspaces;

        public DashboardController()
        {
            this.workspaceCollection = ConfigurationManager.AppSettings["powerbi:WorkspaceCollection"];
            this.workspaces = new ReportsViewModel
            {
                Workspaces = ConfigurationManager.AppSettings["WorkspaceMappings"]
                                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(workspace => new ReportWorkspace
                                {
                                    WorkspaceId = workspace.Split('|')[0],
                                    DisplayName = workspace.Split('|')[1],
                                })
                                .ToList(),
            };
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Reports()
        {
            var viewModel = new ReportsViewModel
            {
                Workspaces = this.workspaces.Workspaces
                    .Select(workspace => new ReportWorkspace
                    {
                        WorkspaceId = workspace.WorkspaceId,
                        DisplayName = workspace.DisplayName,
                        Reports = GetWorkspaceReports(workspace.WorkspaceId),
                    })
                    .ToList(),
            };
            return PartialView(viewModel);
        }

        private IList<Report> GetWorkspaceReports(string workspaceId)
        {
            using (var client = new PowerBIReportClient(this.workspaceCollection, workspaceId))
            {
                try
                {
                    return client.Reports.GetReports(this.workspaceCollection, workspaceId).Value;
                }
                catch
                {
                    return new List<Report>();
                }
            }
        }

        [HttpGet]
        public ActionResult Upload()
        {
            ViewBag.ReportCategory = this.workspaces.Workspaces
                .Select(workspace => new SelectListItem
                {
                    Text = workspace.DisplayName,
                    Value = workspace.WorkspaceId,
                });
            return View(new ReportUploadModel());
        }

        [HttpPost]
        public async Task<ActionResult> Upload(ReportUploadModel report)
        {
            string workspaceId = report.ReportCategory;
            // See if we have a new workspace name
            if (!String.IsNullOrWhiteSpace(report.NewReportCategory))
            {
                using (var client = new PowerBIReportClient(PowerBIToken.CreateProvisionToken(this.workspaceCollection), this.workspaceCollection, String.Empty))
                {
                    var newWorkspaceResult = await client.Workspaces.PostWorkspaceAsync(this.workspaceCollection);
                    this.workspaces.Workspaces.Add(new ReportWorkspace
                    {
                        DisplayName = report.NewReportCategory,
                        WorkspaceId = newWorkspaceResult.WorkspaceId,
                        Reports = new List<Report>(),
                    });
                    workspaceId = newWorkspaceResult.WorkspaceId;
                    // Update web.config
                    var webConfig = WebConfigurationManager.OpenWebConfiguration("~");
                    webConfig.AppSettings.Settings["WorkspaceMappings"].Value = String.Join(";", this.workspaces.Workspaces
                        .Select(workspace => String.Format("{0}|{1}", workspace.WorkspaceId, workspace.DisplayName)));
                    webConfig.Save();
                }
            }
            using (var client = new PowerBIReportClient(this.workspaceCollection, workspaceId))
            {
                var import = await client.Imports.PostImportWithFileAsync(this.workspaceCollection,
                    workspaceId,
                    report.PbixReport.InputStream,
                    report.ReportName);
                import = await client.WaitForImportToComplete(import);
                if (import.ImportState == PowerBIReportClient.StateSucceeded)
                {
                    return RedirectToAction("Report", new { workspaceId = workspaceId, reportId = import.Reports.First().Id });
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Report(string workspaceId, string reportId)
        {
            using (var client = new PowerBIReportClient(this.workspaceCollection, workspaceId))
            {
                var reportsResponse = await client.Reports.GetReportsAsync(this.workspaceCollection, workspaceId);
                var report = reportsResponse.Value.FirstOrDefault(r => r.Id == reportId);
                var embedToken = PowerBIToken.CreateReportEmbedToken(this.workspaceCollection, workspaceId, report.Id);

                var viewModel = new ReportViewModel
                {
                    Report = report,
                    AccessToken = embedToken.Generate(PowerBIReportClient.SigningKey)
                };

                return View(viewModel);
            }
        }

        /*public async Task<JsonResult> ViewReport(string details, string reportId)
        {
            using (var client = new PowerBIReportClient(this.workspaceCollection, this.workspaceId))
            {
                var reportsResponse = await client.Reports.GetReportsAsync(this.workspaceCollection, this.workspaceId);
                var report = reportsResponse.Value.FirstOrDefault(r => r.Id == reportId);
                var embedToken = PowerBIToken.CreateReportEmbedToken(this.workspaceCollection, this.workspaceId, report.Id);

                return Json(new
                {
                    Report = report,
                    AccessToken = embedToken.Generate(PowerBIReportClient.SigningKey)
                });
            }
        }*/
    }
}