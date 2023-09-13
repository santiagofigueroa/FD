using FD.Models;

namespace FD.Interfaces
{
    public interface IPlanEvaluator
    {
        decimal CalculateCost(EnergyPlan plan, int consumption);
        int CalculateEnergy(EnergyPlan plan, decimal spend);
    }
}
