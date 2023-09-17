using FD.Interfaces;
using FD.Models;
using FD.Services;
using Newtonsoft.Json;

public class Program
{
    static void Main()
    {
        Console.Write("input ");
        string filename = Console.ReadLine();
        Console.WriteLine(filename);

        if (!System.IO.File.Exists(filename))
        {
            Console.WriteLine("File not found!");
            return;
        }

        var energyPlans = LoadPlans(filename);
        IPlanEvaluator evaluator = new PlanEvaluatorService();

        Console.WriteLine("Enter command:");

        List<int> consumptions = new List<int>();
        string command;

        // Collect all annual_cost values
        while ((command = Console.ReadLine()) != "exit")
        {
            if (command.StartsWith("annual_cost "))
            {
                int consumption = int.Parse(command.Split(' ')[1]);
                consumptions.Add(consumption);
            }
            else
            {
                Console.WriteLine("Invalid command");
            }

            Console.WriteLine("\nEnter another annual cost or 'exit' to proceed:");
        }

        // Process calculations for each entered annual_cost
        foreach (var consumption in consumptions)
        {
            var results = energyPlans.Select(plan => new
            {
                SupplierName = plan.SupplierName,
                PlanName = plan.PlanName,
                Cost = evaluator.CalculateCost(plan, consumption)
            })
            .OrderBy(x => x.Cost);

            foreach (var result in results)
            {
                Console.WriteLine($"{result.SupplierName},{result.PlanName},{result.Cost}");
            }
        }
    }
    //Json Serialization   
    public static List<EnergyPlan> LoadPlans(string filename)
    {
        string json = System.IO.File.ReadAllText(filename);
        return JsonConvert.DeserializeObject<List<EnergyPlan>>(json);
    }

}