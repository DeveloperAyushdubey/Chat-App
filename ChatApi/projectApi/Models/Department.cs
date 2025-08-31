using System;
using System.Collections.Generic;

namespace projectApi.Models;

public partial class Department
{
    public long Id { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string Remarks { get; set; } = null!;
}
