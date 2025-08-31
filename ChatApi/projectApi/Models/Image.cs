using System;
using System.Collections.Generic;

namespace projectApi.Models;

public partial class Image
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Image1 { get; set; } = null!;
}
