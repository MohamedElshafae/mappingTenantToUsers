using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Interfaces;
using Task.Core.Models;

namespace Task.Data.Repositres
{
    public class UserRepository : IUserRpository
    {
        private readonly WesIdentityContext _context;
        public UserRepository(WesIdentityContext context) =>
            _context = context;
        public AspNetUser GetUserByUsername(string username)
        {
            //Hash logic
            var user = _context.AspNetUsers.Include(u => u.Roles).SingleOrDefault(u => u.UserName == username);
            return user;
        }

        public bool isHasRoleByTanentId(AspNetUser user ,string tanentId) =>
            user.Roles.Any(r => r.TenantId == tanentId);

        public void Save() => _context.SaveChanges();
    }
}
