using System;
using System.Collections.Generic;

namespace DBContext.DB;

public partial class Custom
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public int ProductId { get; set; }

    public string Count { get; set; } = null!;

    public DateTime DateTime { get; set; }
}
