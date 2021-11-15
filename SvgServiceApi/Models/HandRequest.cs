using SvgManipulationLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SvgServiceApi.Models
{
    public class HandRequest
    {
        public double MaxWidth { get; set; }
        public double WidthAtLow { get; set; }
        public double WidthAtHigh { get; set; }
        public double Length { get; set; }
        public List<Region> ConductorRegions { get; set; }

    }
}
