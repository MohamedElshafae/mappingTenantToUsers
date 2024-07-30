using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Interfaces;
using Task.Core.Models;

namespace Task.Data.Repositres
{
    public class RoleRepository : IRoleRepository
    {
        private readonly WesIdentityContext _context;
        public RoleRepository(WesIdentityContext context) =>
            _context = context;

        public AspNetRole createNewRole(string username, string tenantId)
        {
            var role = new AspNetRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = username + tenantId,
                Description = tenantId + '.' + username,
                TenantId = tenantId
            };
            _context.AspNetRoles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public IEnumerable<string> getRolesByTanentId(string tanentId) =>
            _context.AspNetRoles.Where(r => r.TenantId == tanentId).Select(r => r.Id);

        public Dictionary<string, List<AspNetRole>> GetRollesByTenantIds(ICollection<string> tenantIds, string username)
        {
            Dictionary<string, List<AspNetRole>> resRoles = new Dictionary<string, List<AspNetRole>>();
            foreach (var tenantId in tenantIds)
            {
                List<AspNetRole> role = _context.AspNetRoles.Where(r => r.TenantId == tenantId).ToList();
                resRoles.Add(tenantId, role);
            }
            return resRoles;
        }
    }
}
