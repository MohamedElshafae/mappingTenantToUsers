using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Interfaces;
using Task.Core.ServicesInterfaces;
using Task.Core.Models;
using System.Text.Json.Nodes;
using System.Data;
using System.ComponentModel.Design;
using Task.Core.I_OModels;
using System.ComponentModel.DataAnnotations;
using Task.Core.Validation;
using System.Linq.Expressions;
using System.Text.RegularExpressions;


namespace Task.Core.Services
{
    public class AddUserRolesService : IAddUserRolesRepository
    {
        private readonly IUserRpository _userRpository;
        private readonly IRoleRepository _roleRpository;
        private readonly IRoleUserRepository _roleUserRepository;
        private readonly ITanentRepository _tanentRepository;
        public AddUserRolesService(IUserRpository userRpository, IRoleRepository roleRepository,
            IRoleUserRepository roleUserRepository, ITanentRepository tanentRepository)
        {
            _roleRpository = roleRepository;
            _userRpository = userRpository;
            _roleUserRepository = roleUserRepository;
            _tanentRepository = tanentRepository;
        }

        private void userValidationStatus(AspNetUser user , UserResponse res)
        {
            if (user == null)
               res.username = UserValidation.InvalidUser;
            else if (!user.IsActive)
                res.username =  UserValidation.IsNotActive;
            else
                res.username =  user.UserName;
        }

        private bool IsUserHasTanent(string tanentId, string userId)
        {
            var rolesHasSameTanentId = _roleRpository.getRolesByTanentId(tanentId);
            return _roleUserRepository.isHasRoleByUserId(userId, rolesHasSameTanentId);
        }

        private void MakeRolesValidation(Dictionary<string, List<AspNetRole>> tanentRoleDict, UserResponse response, AspNetUser user)
        {
            foreach (var kvp in tanentRoleDict)
            {

                RoleUser userRole = new RoleUser() { UsersId = user.Id };
                var tanentId = kvp.Key;

                if (!_tanentRepository.isExist(tanentId))
                {
                    response.tanentsStatus.Add(tanentId, UserValidation.InvalidTanent);
                    continue;
                }

                bool isUserHasTanent = IsUserHasTanent(tanentId, user.Id);
                string tanentValidation;

                if (isUserHasTanent)
                    tanentValidation = UserValidation.HasThisTenantAlready;
                else
                {
                    var newRole = _roleRpository.createNewRole(user.UserName, tanentId);
                    userRole.RolesId = newRole.Id;
                    _roleUserRepository.AddRoleUser(userRole);
                    tanentValidation = UserValidation.CreateRoleAndSucccesAddToUser;
                }
                response.tanentsStatus.Add(tanentId, tanentValidation);
            }
        }

        /*
         * 1- validate user
         * 2- validate if user have this tanent but not defrent role
         * 3- get rolles of tenant
         * 4- add response
         */
        public List<UserResponse> AddUserRoles(List<UserRequest> requests)
        {
            List<UserResponse> responses = new List<UserResponse>();
            
            foreach (var request in requests)
            {
                // user validation
                UserResponse response = new UserResponse();
                string username = request.Username;
                var user = _userRpository.GetUserByUsername(username);
                userValidationStatus(user, response);

                if (response.username != request.Username)
                {
                    responses.Add(response);
                    continue;
                }
                
                var tanentRoleDict = _roleRpository.GetRollesByTenantIds(request.TenantIds, username);
                MakeRolesValidation(tanentRoleDict, response, user);

                responses.Add(response);
                _roleUserRepository.Save();
            }
            return responses;
        }
    }
}
