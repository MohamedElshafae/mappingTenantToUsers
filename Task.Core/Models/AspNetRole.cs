using System;
using System.Collections.Generic;

namespace Task.Core.Models;

public partial class AspNetRole
{
    public string Id { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string TenantId { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();

    public virtual ICollection<AspNetUser> UsersNavigation { get; set; } = new List<AspNetUser>();
}
