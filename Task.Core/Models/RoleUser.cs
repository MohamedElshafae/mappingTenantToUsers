using System.ComponentModel.DataAnnotations.Schema;

namespace Task.Core.Models
{
    public class RoleUser
    {
        [ForeignKey("Role")]
        public string RolesId { get; set; }

        [ForeignKey("User")]
        public string UsersId { get; set; }

        public AspNetUser? User { get; set; } = null;
        public AspNetRole? Role { get; set; } = null!;
    }
}
