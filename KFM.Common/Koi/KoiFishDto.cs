using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFM.Common.Koi
{
    public class KoiFishDto
    {
        public int KoiId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string BodyShape { get; set; }

        public int? Age { get; set; }

        public double? Size { get; set; }

        public double? Weight { get; set; }

        public string Gender { get; set; }

        public string Breed { get; set; }

        public string Origin { get; set; }

        public decimal? Price { get; set; }

        public int? PondId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
