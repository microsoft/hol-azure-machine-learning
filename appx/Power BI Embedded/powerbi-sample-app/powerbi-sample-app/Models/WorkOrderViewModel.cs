using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace powerbi_sample_app.Models
{
    public class WorkOrderViewModel
    {
        public string WorkOrderName { get; set; }
        public string AircraftName { get; set; }
        public string BlockingWorkOrders { get; set; }
        public DateTime RequiredCompletionDate { get; set; }
        public List<Discrepancy> Discrepancies { get; set; }
    }

    public class Discrepancy
    {
        public string State { get; set; }
        public string DiscrepancyType { get; set; }
        public string Reporter { get; set; }
        public string Description { get; set; }
        public double Hours { get; set; }
        public bool Complete { get; set; }
    }
}