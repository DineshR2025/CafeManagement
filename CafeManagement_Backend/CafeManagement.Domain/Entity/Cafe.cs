using System;
using System.Collections.Generic;

namespace CafeManagement.Domain.Entity;

public partial class Cafe
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte[]? Logo { get; set; }

    public string Location { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
