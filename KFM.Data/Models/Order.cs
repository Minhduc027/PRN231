﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KFM.Data.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string ProductName { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime? CreatedAt { get; set; }
}