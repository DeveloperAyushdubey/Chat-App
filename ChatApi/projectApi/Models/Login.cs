using System;
using System.Collections.Generic;

namespace projectApi.Models;

public partial class Login
{
    public int Id { get; set; }

    public string? MobileNo { get; set; }

    public string? Password { get; set; }
}
