using System;
using System.Collections.Generic;

namespace projectApi.Models;

public partial class Student
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Class { get; set; } = null!;

    public int Age { get; set; }

    public long DepartmentId { get; set; }
}
