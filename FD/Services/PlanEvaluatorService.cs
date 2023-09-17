using FD.Interfaces;
using FD.Models;

namespace FD.Services
{
    public class PlanEvaluatorService : IPlanEvaluator
    {
        //VAT 5% 
        private const decimal VAT = 0.05m;

        public decimal CalculateCost(EnergyPlan plan, int consumption)
        {
            // Convert daily standing charge from pence to pounds and then multiply by 365 of the year
            decimal annualStandingCharge = (plan.StandingCharge / 100.0m) * 365;   
            decimal cost = annualStandingCharge;
            int remainingConsumption = consumption;

            foreach (var price in plan.Prices)
            {
                decimal rateInPounds = price.Rate / 100.0m;  // Convert rate from pence to pounds
                if (!price.Threshold.HasValue || remainingConsumption <= price.Threshold)
                {
                    cost += rateInPounds * remainingConsumption;
                    break;
                }
                else
                {
                    cost += rateInPounds * price.Threshold.Value;
                    remainingConsumption -= price.Threshold.Value;
                }
            }

            cost *= (1 + VAT);
            return Math.Round(cost, 2);
        }
    }
}
