using System;
using System.Collections.Generic;

namespace DBContext.DB;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public long Price { get; set; }

    public string Country { get; set; } = null!;

    public decimal? Count { get; set; }
}
