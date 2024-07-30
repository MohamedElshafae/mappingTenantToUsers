using System;
using System.Collections.Generic;

namespace Task.Core.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string EnNormalizedName { get; set; } = null!;

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
