using FD.Interfaces;
using FD.Models;

namespace FD.Services
{
    public class PlanEvaluatorService : IPlanEvaluator
    {
        private const decimal VAT = 0.05m;

        public decimal CalculateCost(EnergyPlan plan, int consumption)
        {
            decimal cost = plan.StandingCharge;
            int remainingConsumption = consumption;

            foreach (var price in plan.Prices)
            {
                if (!price.Threshold.HasValue || remainingConsumption <= price.Threshold)
                {
                    cost += price.Rate * remainingConsumption;
                    break;
                }
                else
                {
                    cost += price.Rate * price.Threshold.Value;
                    remainingConsumption -= price.Threshold.Value;
                }
            }

            return cost * (1 + VAT);
        }

        public int CalculateEnergy(EnergyPlan plan, decimal spend)
        {
            decimal remainingSpend = (spend / (1 + VAT)) - plan.StandingCharge;
            int totalEnergy = 0;

            foreach (var price in plan.Prices)
            {
                if (!price.Threshold.HasValue || remainingSpend < price.Rate * price.Threshold)
                {
                    totalEnergy += (int)(remainingSpend / price.Rate);
                    break;
                }
                else
                {
                    totalEnergy += price.Threshold.Value;
                    remainingSpend -= price.Rate * price.Threshold.Value;
                }
            }

            return totalEnergy;
        }
    }
}
