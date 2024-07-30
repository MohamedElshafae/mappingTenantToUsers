using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Models;

namespace Task.Core.Interfaces
{
    public interface IRoleUserRepository
    {
        public bool isHasRoleByUserId(string userId, IEnumerable<string> roleIds);
        void AddRoleUser(RoleUser roleUser);
        void Save();
    }
}
