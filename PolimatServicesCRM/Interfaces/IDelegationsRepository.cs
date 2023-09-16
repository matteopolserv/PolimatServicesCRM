using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Interfaces
{
    public interface IDelegationsRepository
    {
        Task<bool> AddDelegation(DelegationModel delegation);
        Task<bool> RemoveDelegation(DelegationModel delegation);
        Task<bool> UpdateDelegation(DelegationModel delegation);
        bool DelegationExists(string delegationId);
        Task<List<DelegationModel>> GetAllDelegations();
        Task<DelegationModel> GetDelegationById(string delegationId);
    }
}
