using Microsoft.JSInterop;
using PolimatServicesCRM.Models;

namespace PolimatServicesCRM.Pages
{
    public partial class Delegations
    {
        DelegationModel delegation = new();
        List<DelegationModel> delegations;
        bool showDetails = false;
        bool addDelegation = false;
        bool editDelegation = false;
        bool? succesfullyAdded = null;
        bool? succesfullySaved = null;
        bool? succesfullyDeleted = null;

        protected override async Task OnInitializedAsync()
        {
            delegations = await _delegations.GetAllDelegations();
            base.OnInitialized();
        }
        private void ShowDetails(int index)
        {
            Reset();
            showDetails = true;
            delegation = delegations[index];
        }
        private void EditDelegation(int index)
        {
            Reset();
            editDelegation = true;
            delegation = delegations[index];
        }
        private void AddDelgation()
        {
            Reset();
            addDelegation = true;
            delegation = new();
        }
        private void Reset()
        {
            editDelegation = false;
            showDetails = false;
            addDelegation = false;
            delegation = new();
        }
        private async Task AddDelegationSubmitHandle()
        {
            bool added = await _delegations.AddDelegation(delegation);
            succesfullyAdded = added;
            if (added) delegation = new();

            await OnInitializedAsync();

        }
        private async Task EditDelegationSubmitHandle()
        {
            bool saved = await _delegations.UpdateDelegation(delegation);
            succesfullySaved = saved;
            await OnInitializedAsync();

        }
        private async Task DeleteDelegationSubmitHandle()
        {
            bool deleted = await _delegations.RemoveDelegation(delegation);
            succesfullyDeleted = deleted;
            await OnInitializedAsync();
        }

        private async Task DownloadDelegation(int index)
        {
            PdfFileModel delegationFile = await _generatePDF.GenerateDelegationPdf(delegations[index].DelegationId);
            Stream stream = new MemoryStream(delegationFile.Data);
            using var streamRef = new DotNetStreamReference(stream);
            await JS.InvokeVoidAsync("downloadFileFromStream", delegationFile.Name, streamRef);
        }


    }
}
