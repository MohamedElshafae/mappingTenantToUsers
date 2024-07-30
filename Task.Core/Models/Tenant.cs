using System;
using System.Collections.Generic;

namespace Task.Core.Models;

public partial class Tenant
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Logo { get; set; }

    public bool IsActive { get; set; }

    public bool IsVirtual { get; set; }

    public string? OwnerId { get; set; }

    public string CountryCode { get; set; } = null!;

    public virtual ICollection<AspNetRole> AspNetRoles { get; set; } = new List<AspNetRole>();

    public virtual Owner? Owner { get; set; }
}
