using powerbi_sample_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace powerbi_sample_app.Controllers
{
    public class WorkOrderController : Controller
    {
        private WorkOrderViewModel sampleModel;

        public WorkOrderController()
        {
            this.sampleModel = new WorkOrderViewModel()
            {
                WorkOrderName = "",
                AircraftName = "CONTOSO357",
                BlockingWorkOrders = "CON1423, CON2971",
                RequiredCompletionDate = new DateTime(2016, 3, 31),
                Discrepancies = new List<Discrepancy>()
                {
                    new Discrepancy()
                    {
                        State = "Immediate",
                        DiscrepancyType = "Anomaly",
                        Reporter = "James Baker",
                        Description = "Significant engine second stage pressure anomaly.",
                        Hours = 4.5,
                        Complete = false
                    },
                    new Discrepancy()
                    {
                        State = "Required",
                        DiscrepancyType = "Maintenance Message",
                        Reporter = "James Baker",
                        Description = "Maintenance Message combination suggests significant issues.",
                        Hours = 9.2,
                        Complete = false
                    },
                    new Discrepancy()
                    {
                        State = "In Maintenance",
                        DiscrepancyType = "Recommendation",
                        Reporter = "Lara Rubbelke",
                        Description = "Fuel consumption increase predicted, engine wash recommended.",
                        Hours = 1.2,
                        Complete = false
                    }
                }
            };
        }

        public ActionResult Create()
        {
            return View(this.sampleModel);
        }

        // GET: WorkOrder
        public ActionResult Details()
        {
            return View(this.sampleModel);
        }
    }
}