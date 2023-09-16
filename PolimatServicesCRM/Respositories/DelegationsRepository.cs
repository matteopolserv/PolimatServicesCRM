using Microsoft.EntityFrameworkCore;
using PolimatServicesCRM.Data;
using PolimatServicesCRM.Interfaces;
using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Respositories
{
    public class DelegationsRepository : IDelegationsRepository
    {
        private readonly ApplicationDbContext _ctx;
        public DelegationsRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> AddDelegation(DelegationModel delegation)
        {
            delegation.DelegationId = Guid.NewGuid().ToString();
            delegation.SetValues();
            await _ctx.AddAsync(delegation);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public bool DelegationExists(string delegationId)
        {
            return _ctx.Delegations.Any(del => del.DelegationId.Equals(delegationId));
        }

        public async Task<List<DelegationModel>> GetAllDelegations()
        {
            return await _ctx.Delegations.OrderByDescending(del => del.DelegationStartDate).ToListAsync();
        }

        public async Task<DelegationModel> GetDelegationById(string delegationId)
        {
            return await _ctx.Delegations.FirstOrDefaultAsync(del => del.DelegationId.Equals(delegationId));
        }

        public async Task<bool> RemoveDelegation(DelegationModel delegation)
        {
            _ctx.Delegations.Remove(delegation);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateDelegation(DelegationModel delegation)
        {
            delegation.SetValues();
            _ctx.Delegations.Update(delegation);
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
