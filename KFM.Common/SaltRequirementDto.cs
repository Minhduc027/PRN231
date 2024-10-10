using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFM.Common;

public class SaltRequirementDto
{
    public int SaltId { get; set; }

    public int? PondId { get; set; }

    public double? RequiredSaltAmount { get; set; }

    public string Notes { get; set; }

    public DateTime? CreatedAt { get; set; }
    public PondDto Pond { get; set; } = new PondDto();
}
