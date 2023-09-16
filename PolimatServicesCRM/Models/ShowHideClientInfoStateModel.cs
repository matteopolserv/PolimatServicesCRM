using Newtonsoft.Json.Linq;
using System.Drawing;

namespace PolimatServicesCRM.Models
{
    public class ShowHideClientInfoStateModel
    {
        public bool ShowClientInfo { get; set; }
        public bool ShowClientEdit { get; set; }
        public bool ShowAddClient { get; set; }
        public string ClientId { get; set; }=string.Empty;

        public event Action OnStateChange;

        public void SetShowValue(bool value, string clientId)
        {
            ShowClientInfo = value;
            ClientId = clientId;
            NotifyStateChanged();
        }
        public void SetEditValue(bool value, string clientId)
        {
            ShowClientEdit = value;
            ClientId = clientId;
            NotifyStateChanged();
        }
        public void SetAddValue(bool value)
        {
            ShowAddClient = value;
            
            NotifyStateChanged();
        }
        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
