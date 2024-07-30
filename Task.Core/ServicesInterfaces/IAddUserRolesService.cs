using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Task.Core.I_OModels;
using Task.Core.Services;

namespace Task.Core.ServicesInterfaces
{
    public interface IAddUserRolesRepository
    {
        List<UserResponse> AddUserRoles(List<UserRequest> requests);
    }
}
