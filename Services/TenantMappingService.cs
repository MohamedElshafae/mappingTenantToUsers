using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Interfaces;
using Task.Core.Models;

namespace Task.Core.Services
{
    public class TenantMappingService : ITenantMappingService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IOwnerRepository _ownerRepository;
        public TenantMappingService(ITenantRepository tenantRepository, IOwnerRepository ownerRepository)
        {
            _tenantRepository = tenantRepository;
            _ownerRepository = ownerRepository;
        }
        public IEnumerable<Tenant> MapAllTenantsToOwner(IEnumerable<string> Ids, string ownerId)
        {
            var tenantsWillMap = _tenantRepository.GetTenantsByIds(Ids);
            foreach (var tenant in tenantsWillMap)
                tenant.OwnerId = ownerId;
            _tenantRepository.Save(tenantsWillMap);
            return tenantsWillMap;
        }

    }
}
