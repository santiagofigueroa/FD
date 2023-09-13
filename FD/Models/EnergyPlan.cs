using System.Diagnostics;

namespace FD.Models
{
    public class EnergyPlan
    {
        public string SupplierName { get; set; }
        public string PlanName { get; set; }
        public List<Price> Prices { get; set; }
        public decimal StandingCharge { get; set; }
    }
}
