using Microsoft.PowerBI.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace powerbi_sample_app.Models
{
    public class ReportViewModel
    {
        public IReport Report { get; set; }

        public string AccessToken { get; set; }
    }
}