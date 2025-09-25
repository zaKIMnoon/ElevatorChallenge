using ElevatorChallengeLib.Models;
using ElevatorChallengeLib.Models.Enums;

namespace ElevatorChallengeLib.Managers
{
    /// <summary>
    /// Manages all elevators in the system, assigning them to requests
    /// based on availability, proximity, and direction.
    /// </summary>
    public class ElevatorManager
    {
        private List<Elevator> Elevators;
        private Queue<FloorRequest> FloorRequests = new Queue<FloorRequest>();
        private object _lock = new object();
        private bool _running = true;
        private int MaxFloors;

        /// <summary>
        /// Initializes a new ElevatorManager with a list of elevators and maximum floor limit.
        /// </summary>
        /// <param name="elevators">The elevators managed by the system.</param>
        /// <param name="maxFloors">The maximum number of floors in the building.</param>
        public ElevatorManager(List<Elevator> elevators, int maxFloors)
        {
            Elevators = elevators;
            MaxFloors = maxFloors;
        }

        /// <summary>
        /// Queues a new request for an elevator from a specific floor and number of people.
        /// </summary>
        /// <param name="floor">The floor number where the request is made.</param>
        /// <param name="peopleCount">The number of people requesting the elevator.</param>
        public void RequestElevator(int floor, int peopleCount)
        {

            if (floor < 0 || floor > MaxFloors)
            {
                Console.WriteLine($"Invalid request: floor {floor} does not exist. Building has 0–{MaxFloors} floors.");
                return;
            }

            // Guess direction (in real-world UI, the user presses Up or Down button)
            DirectionState dir = floor >= 0 ? DirectionState.Up : DirectionState.Down;

            lock (_lock)
            {
                FloorRequests.Enqueue(new FloorRequest
                {
                    Floor = floor,
                    PeopleCount = peopleCount,
                    RequestedDirection = dir
                });
            }

            Task.Run(() => ProcessRequests());
        }

        /// <summary>
        /// Stops the elevator system and halts further processing.
        /// </summary>
        public void Stop()
        {
            _running = false;
            Console.WriteLine("Elevator system shutting down...");
        }

        /// <summary>
        /// Processes pending requests by assigning elevators to them
        /// based on direction, availability, and proximity.
        /// </summary>
        private void ProcessRequests()
        {
            while (_running)
            {
                FloorRequest request = null;
                Elevator chosenElevator = null;

                lock (_lock)
                {
                    if (!FloorRequests.Any()) return;

                    var pending = FloorRequests.Peek();

                    // 1. Look for an elevator moving in the same direction & on the way
                    chosenElevator = Elevators.FirstOrDefault(e =>
                        e.State == ElevatorState.Moving &&
                        e.DirectionState == pending.RequestedDirection &&
                        (e.DirectionState == DirectionState.Up && e.CurrentFloor <= pending.Floor ||
                         e.DirectionState == DirectionState.Down && e.CurrentFloor >= pending.Floor));

                    // 2. If none, pick the closest idle elevator
                    if (chosenElevator == null)
                    {
                        chosenElevator = Elevators
                            .Where(e => e.State == ElevatorState.Idle)
                            .OrderBy(e => Math.Abs(e.CurrentFloor - pending.Floor))
                            .FirstOrDefault();
                    }

                    if (chosenElevator == null) return; // No elevator available yet

                    request = FloorRequests.Dequeue();
                }

                if (request != null && chosenElevator != null)
                {
                    HandleRequest(chosenElevator, request);
                }
            }
        }

        /// <summary>
        /// Handles an elevator request, accounting for capacity and returning
        /// multiple times if necessary until all waiting passengers are picked up.
        /// </summary>
        /// <param name="elevator">The elevator assigned to the request.</param>
        /// <param name="request">The request to handle.</param>
        private void HandleRequest(Elevator elevator, FloorRequest request)
        {
            Task.Run(async () =>
            {
                int remainingPeople = request.PeopleCount;

                while (remainingPeople > 0 && _running)
                {
                    int taking = Math.Min(elevator.Capacity, remainingPeople);
                    Console.WriteLine($"{elevator.Name} assigned to floor {request.Floor}. Picking up {taking} people.");

                    await elevator.MoveToFloor(request.Floor, MaxFloors);

                    remainingPeople -= taking;

                    if (remainingPeople > 0)
                    {
                        Console.WriteLine($"{remainingPeople} people still waiting at floor {request.Floor}. {elevator.Name} will return.");
                    }
                }
            });
        }
    }
}
