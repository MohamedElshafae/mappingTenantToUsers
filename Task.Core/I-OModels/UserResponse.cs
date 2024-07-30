using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Core.I_OModels
{
    public class UserResponse
    {
        public string username { get; set; }
        public Dictionary<string, string>? tanentsStatus { get; set; } = new Dictionary<string, string>();

    }
}
