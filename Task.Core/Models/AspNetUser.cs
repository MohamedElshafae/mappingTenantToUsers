using System;
using System.Collections.Generic;

namespace Task.Core.Models;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public string? FullName { get; set; }

    public string OwnerId { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public DateTime? LastLoginTime { get; set; }

    public int? LoginCount { get; set; }

    public bool IsActive { get; set; }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public bool? LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual Owner Owner { get; set; } = null!;

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();

    public virtual ICollection<AspNetRole> RolesNavigation { get; set; } = new List<AspNetRole>();
}
