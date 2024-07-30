using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Interfaces;

namespace Task.Data.Repositres
{
    public class TanentRepository : ITanentRepository
    {
        WesIdentityContext _context;
        public TanentRepository(WesIdentityContext context) =>
            _context = context;
        public bool isExist(string id) =>
            _context.Tenants.Any(t => t.Id == id);
    }
}
