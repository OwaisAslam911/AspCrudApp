using System;
using System.Collections.Generic;

namespace AspCrudApp.Models;

public partial class Product
{
    public int Pid { get; set; }

    public string Pname { get; set; } = null!;

    public string Pdescription { get; set; } = null!;

    public decimal Pprice { get; set; }

    public int Pquantity { get; set; }

    public string Image { get; set; } = null!;
}
