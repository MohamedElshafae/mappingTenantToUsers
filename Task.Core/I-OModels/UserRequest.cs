using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Core.I_OModels
{
    public class UserRequest
    {
        public string Username { get; set; }
        public List<string> TenantIds { get; set; }
    }
}
