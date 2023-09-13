using FD.Interfaces;
using FD.Models;
using FD.Services;
using Newtonsoft.Json;

static void Main()
{
    Console.WriteLine("Please enter the filename of the energy plan (e.g., plan.json):");
    string filename = Console.ReadLine();

    if (!System.IO.File.Exists(filename))
    {
        Console.WriteLine("File not found!");
        return;
    }

    string json = System.IO.File.ReadAllText(filename);
    var energyPlan = JsonConvert.DeserializeObject<EnergyPlan>(json);
    IPlanEvaluator evaluator = new PlanEvaluatorService();

    var cost = evaluator.CalculateCost(energyPlan, 300);
    Console.WriteLine($"Cost for 300 kWh: £{cost}");

    var energy = evaluator.CalculateEnergy(energyPlan, cost);
    Console.WriteLine($"Energy for £{cost}: {energy} kWh");
}