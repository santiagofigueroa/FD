using FD.Interfaces;
using FD.Models;
using FD.Services;
using Newtonsoft.Json;
public class Program
{
    static List<EnergyPlan> LoadPlans(string filename)
    {
        string json = System.IO.File.ReadAllText(filename);
        return JsonConvert.DeserializeObject<List<EnergyPlan>>(json);
    }

    static void Main()
    {
        Console.WriteLine("Please enter the filename of the energy plans (e.g., plans.json):");
        string filename = "C:\\Users\\santi\\source\\repos\\FD\\RiskTechnicalTest\\plans.json"; //s Console.ReadLine();

        if (!System.IO.File.Exists(filename))
        {
            Console.WriteLine("File not found!");
            return;
        }

        var energyPlans = LoadPlans(filename);
        IPlanEvaluator evaluator = new PlanEvaluatorService();

        Console.WriteLine("Enter command:");
        string command = Console.ReadLine();

        while (command != "exit")
        {
            if (command.StartsWith("annual_cost "))
            {
                int consumption = int.Parse(command.Split(' ')[1]);

                var results = energyPlans.Select(plan => new
                {
                    SupplierName = plan.SupplierName,
                    PlanName = plan.PlanName,
                    Cost = evaluator.CalculateCost(plan, consumption)
                })
                .OrderBy(x => x.Cost)
                .Take(4); // Assuming you want to list the top 4 plans based on the provided example

                foreach (var result in results)
                {
                    Console.WriteLine($"{result.SupplierName},{result.PlanName},{result.Cost:F2}");
                }
            }
            else
            {
                Console.WriteLine("Invalid command");
            }
        }
    }
}