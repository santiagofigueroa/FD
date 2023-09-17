using System.Diagnostics;
using FD.Interfaces;
using Newtonsoft.Json;

namespace FD.Models
{
    public class EnergyPlan : IEnergyPlan
    {
        [JsonProperty("supplier_name")]
        public string SupplierName { get; set; }

        [JsonProperty("plan_name")]
        public string PlanName { get; set; }
        public List<Price> Prices { get; set; }
        [JsonProperty("standing_charge")]
        public decimal StandingCharge { get; set; }
    }
}
