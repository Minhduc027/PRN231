using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFM.Common
{
    public class WaterParameterDto
    {
        public int ParameterId { get; set; }

        public int? PondId { get; set; }

        public DateOnly? DateMeasured { get; set; }

        public double? Temperature { get; set; }

        public double? SaltLevel { get; set; }

        public double? Phlevel { get; set; }

        public double? O2level { get; set; }

        public double? No2level { get; set; }

        public double? No3level { get; set; }

        public double? Po4level { get; set; }

        public string Recommendation { get; set; }

        public DateTime? CreatedAt { get; set; }

        public PondDto Pond { get; set; } = new PondDto();
    }
}
