using System;
using System.Collections.Generic;

namespace Task.Core.Models;

public partial class Owner
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsVirtual { get; set; }

    public virtual ICollection<AspNetUser> AspNetUsers { get; set; } = new List<AspNetUser>();

    public virtual ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
}
