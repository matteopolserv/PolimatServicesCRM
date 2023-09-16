using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace PolimatServicesCRM.Models
{
    public class DelegationModel
    {
        [Key]
        public string DelegationId { get; set; } = string.Empty;
        public string DelegatedId { get; set; } = string.Empty;
        public string DelegatedName { get; set; } = string.Empty;   
        public string DelegatedPost { get; set; } = string.Empty;
        public string DelegatingId { get; set; } = string.Empty;
        public string DelegatingName { get; set; } = string.Empty;
        public string DelegatingPost { get; set; } = string.Empty;
        public string Vechicle { get; set; } = "Samochó prywatny - Renault Koleos 2.0 dci";
        public DateTime DelegationStartDate { get; set; } = DateTime.Now;
        public DateTime DelegationEndDate { get; set; } = DateTime.Now;
        public string DeleagationDeparturePlace { get; set;} = "Kielnarowa 217d, 36-020";
        public string DeleagationArrivngPlace { get; set;} = string.Empty;
        public string DelegationPurpose { get; set;} = string.Empty;
        public decimal Distance { get; set;}
        public int DeleagationDuringTime { get; set; }
       
       
        public decimal DeleagationPerDiemRate { get; set; } = 45;
        public decimal DeleagationPerDiemTotal { get; set; }
     
        public decimal DelegationOvernightStay { get; set; }
        
        public decimal DistanceDistanceRate { get; set; } = 1.15m;
        public decimal DistanceTotalCost { get; set; }
      
       
        public decimal DelegationTotalCost { get; set; }
     

        public void SetValues()
        {
            TimeSpan duringTime = DelegationStartDate - DelegationEndDate;
            DeleagationDuringTime = Convert.ToInt32(duringTime.TotalHours);
            
            DistanceTotalCost = Distance * DistanceDistanceRate;
            if (DeleagationDuringTime < 8)
            {
                DeleagationPerDiemTotal = 0;
            }
            else if (DeleagationDuringTime >= 8 && DeleagationDuringTime < 12)
            {
                DeleagationPerDiemTotal = 0.5m * DeleagationPerDiemRate;
            }
            else if (DeleagationDuringTime >= 12 && DeleagationDuringTime < 24)
            {
                DeleagationPerDiemTotal = DeleagationPerDiemRate;
            }
            else if (DeleagationDuringTime >= 24)
            {
                DeleagationPerDiemTotal = DeleagationDuringTime / 24 * DeleagationPerDiemRate;
                if (DeleagationDuringTime % 24 < 8) DeleagationPerDiemTotal += 0.5m * DeleagationPerDiemRate;
                else DeleagationPerDiemTotal += DeleagationPerDiemRate;
            }
            DelegationTotalCost = DistanceTotalCost + DelegationOvernightStay + DeleagationPerDiemTotal;

        }
    }
}
