using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Models;

namespace Task.Core.Interfaces
{
    public interface IRoleRepository
    {
        AspNetRole createNewRole(string username, string tenantId);
        Dictionary<string, List<AspNetRole>> GetRollesByTenantIds(ICollection<string> tenantIds, string username);
        IEnumerable<string> getRolesByTanentId(string tanentId);
    }
}
