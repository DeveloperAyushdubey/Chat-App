using System;
using System.Collections.Generic;

namespace projectApi.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string EmployeeCode { get; set; } = null!;

    public string Departmenet { get; set; } = null!;

    public string Role { get; set; } = null!;
}
