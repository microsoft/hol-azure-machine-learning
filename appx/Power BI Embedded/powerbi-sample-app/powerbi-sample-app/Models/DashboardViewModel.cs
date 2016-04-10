using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Beta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace paas_demo.Models
{
    public class DashboardViewModel
    {
        public IDashboard Dashboard { get; set; }

        public List<Tile> Tiles { get; set; }
    }
}