using NUnit.Framework;
using FD.Models;
using FD.Services;
using System.Collections.Generic;
using FD.Interfaces;
using Newtonsoft.Json;

namespace FD.Tests
{
    public class PlanEvaluatorTests
    {
        private List<EnergyPlan> _energyPlans;
        private IPlanEvaluator _evaluator;

        [SetUp]
        public void Setup()
        {
            // Assume you have a method to load plans like the one in your Program class
            _energyPlans = Program.LoadPlans("C:\\Users\\santi\\source\\repos\\FD\\RiskTechnicalTest\\plans.json");
            _evaluator = new PlanEvaluatorService();
        }

        [Test]
        public void TestAnnualCost1000()
        {
            var results = GetTopPlansForConsumption(1000);

            AssertResults(results[0], "energyOne", "planOne", 108.68M);
            AssertResults(results[1], "energyThree", "planThree", 111.25M);
            AssertResults(results[2], "energyTwo", "planTwo", 120.22M);
            AssertResults(results[3], "energyFour", "planFour", 121.33M);
        }

        [Test]
        public void TestAnnualCost2000()
        {
            var results = GetTopPlansForConsumption(2000);

            AssertResults(results[0], "energyThree", "planThree", 205.75M);
            AssertResults(results[1], "energyOne", "planOne", 213.68M);
            AssertResults(results[2], "energyFour", "planFour", 215.83M);
            AssertResults(results[3], "energyTwo", "planTwo", 235.72M);
        }

        private void AssertResults((string supplier, string plan, decimal cost) result, string expectedSupplier, string expectedPlan, decimal expectedCost)
        {
            Assert.AreEqual(expectedSupplier, result.supplier);
            Assert.AreEqual(expectedPlan, result.plan);
            Assert.AreEqual(expectedCost, result.cost);
        }

        private List<(string SupplierName, string PlanName, decimal Cost)> GetTopPlansForConsumption(int consumption)
        {
            var results = _energyPlans.Select(plan => new
            {
                SupplierName = plan.SupplierName,
                PlanName = plan.PlanName,
                Cost = _evaluator.CalculateCost(plan, consumption)
            })
            .OrderBy(x => x.Cost)
            .Select(x => (x.SupplierName, x.PlanName, x.Cost))
            .ToList();

            return results;
        }

        [Test]
        public void TestOrderForAnnualCost1000()
        {
            var expectedResult = new List<(string SupplierName, string PlanName, decimal Cost)>
            {
                ("energyOne", "planOne", 108.68m),
                ("energyThree", "planThree", 111.25m),
                ("energyTwo", "planTwo", 120.22m),
                ("energyFour", "planFour", 121.33m)
            };

            var results = GetTopPlansForConsumption(1000);

            Assert.True(results.SequenceEqual(expectedResult));
        }

        [Test]
        public void TestOrderForAnnualCost2000()
        {
            var expectedResult = new List<(string SupplierName, string PlanName, decimal Cost)>
            {
                ("energyThree", "planThree", 205.75m),
                ("energyOne", "planOne", 213.68m),
                ("energyFour", "planFour", 215.83m),
                ("energyTwo", "planTwo", 235.72m)
            };

            var results = GetTopPlansForConsumption(2000);

            Assert.True(results.SequenceEqual(expectedResult));
        }
    }
}
