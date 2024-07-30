using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;
using Task.Core.I_OModels;
using Task.Core.Services;
using Task.Core.ServicesInterfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAddUserRolesRepository _addUserRolesService;
        public UserController(IAddUserRolesRepository addUserRolesService) =>
            _addUserRolesService = addUserRolesService;

        // POST api/<UserController>
        [HttpPost]
        public ActionResult Post([FromBody] List<UserRequest> requests)
        {
            var response = _addUserRolesService.AddUserRoles(requests);
            return (Ok(response));
        }
    }
    
}
