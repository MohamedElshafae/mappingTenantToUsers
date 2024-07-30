using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Interfaces;
using Task.Core.Models;

namespace Task.Data.Repositres
{
    public class RoleUserRepository : IRoleUserRepository
    {
        WesIdentityContext _context;
        public RoleUserRepository(WesIdentityContext context) =>
            _context = context;
        public void AddRoleUser(RoleUser roleUser) =>
            _context.RoleUsers.Add(roleUser);



        public bool isHasRoleByUserId(string userId, IEnumerable<string> roleIds) =>
            _context.RoleUsers.Any(ru => (ru.UsersId == userId && roleIds.Contains(ru.RolesId)));
        public void Save() => 
            _context.SaveChanges();

    }
}
