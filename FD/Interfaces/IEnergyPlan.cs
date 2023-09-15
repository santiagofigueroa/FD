using FD.Models;

namespace FD.Interfaces
{
    public interface IEnergyPlan
    {

        string SupplierName { get; set; }
         string PlanName { get; set; }
         List<Price> Prices { get; set; }
         decimal StandingCharge { get; set; }
    }
}