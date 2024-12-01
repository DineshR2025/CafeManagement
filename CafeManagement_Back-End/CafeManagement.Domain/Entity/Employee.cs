using System;
using System.Collections.Generic;

namespace CafeManagement.Domain.Entity;

public partial class Employee
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public Guid CafeId { get; set; }

    public DateOnly StartDate { get; set; }

    public virtual Cafe Cafe { get; set; } = null!;
}
