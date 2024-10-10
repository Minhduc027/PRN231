using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFM.Common;

public class PondDto
{
    public int PondId { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public double? Size { get; set; }

    public double? Depth { get; set; }

    public double? Volume { get; set; }

    public int? DrainCount { get; set; }

    public double? PumpCapacity { get; set; }

    public string Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
