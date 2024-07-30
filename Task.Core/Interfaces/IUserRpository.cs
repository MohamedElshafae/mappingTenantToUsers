using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Models;

namespace Task.Core.Interfaces
{
    public interface IUserRpository
    {
        AspNetUser GetUserByUsername(string username);
        bool isHasRoleByTanentId(AspNetUser user, string tanentId);
    }
}
