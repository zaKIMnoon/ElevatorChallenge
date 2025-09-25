using ElevatorChallengeLib.Managers;
using ElevatorChallengeLib.Models.Config;
using System.Text.Json;

namespace ElevatorChallenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string jsonString = File.ReadAllText("config/elevatorConfig.json");
            var building = JsonSerializer.Deserialize<BuildingConfig>(jsonString);

            Console.WriteLine($"Building Name: {building.BuildingName}");
            Console.WriteLine($"Number of Floors: {building.NumberOfFloors}");

            var manager = new ElevatorManager(building.ElevatorList, building.NumberOfFloors);

            Console.WriteLine("Elevator system started.");

            building.ElevatorList.ForEach(e =>
            {
                Console.WriteLine($"  Elevator: {e.Name}, Speed: {e.SpeedPerSecond} floors/per second, Capacity: {e.Capacity}, Current Floor: {e.CurrentFloor}");
            });

            Console.WriteLine("Commands:");
            Console.WriteLine("  request <floor> <people>  -> to request an elevator");
            Console.WriteLine("  exit                      -> to stop the system");

            string input;
            while ((input = Console.ReadLine()) != null)
            {
                var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 0) continue;

                if (parts[0].Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                    parts[0].Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                    manager.Stop();
                    break;
                }
                else if (parts[0].Equals("request", StringComparison.OrdinalIgnoreCase) && parts.Length == 3)
                {
                    if (int.TryParse(parts[1], out int floor) &&
                        int.TryParse(parts[2], out int people))
                    {
                        manager.RequestElevator(floor, people);
                    }
                    else
                    {
                        Console.WriteLine("Invalid request format. Use: request <floor> <people>");
                    }
                }
                else
                {
                    Console.WriteLine("Unknown command.");
                }
            }

            Console.WriteLine("System exited.");
        }
    }
}
