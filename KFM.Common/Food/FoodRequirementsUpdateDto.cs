using KFM.Common.Koi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFM.Common.Food
{
    public class FoodRequirementsUpdateDto
    {
        public int FoodId { get; set; }

        public int? KoiId { get; set; }

        public string DevelopmentStage { get; set; }

        public double? RequiredAmount { get; set; }

        public string Notes { get; set; }

        
        public KoiFishDto Koi { get; set; } = new KoiFishDto();
    }
}
