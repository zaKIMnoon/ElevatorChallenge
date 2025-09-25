using ElevatorChallengeLib.Models.Enums;

namespace ElevatorChallengeLib.Models
{
    /// <summary>
    /// Represents a single elevator with properties like capacity, speed, and current floor.
    /// </summary>
    public class Elevator
    {
        /// <summary>
        /// The Name of the elevator e.g., "E1", "E2").
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Speed of the elevator in floors per second.
        /// </summary>
        public decimal SpeedPerSecond { get; set; }

        /// <summary>
        /// Current floor where the elevator is located.
        /// </summary>
        public int CurrentFloor { get; set; }

        /// <summary>
        /// Maximum number of people the elevator can carry at one time.
        /// </summary>
        public int Capacity { get; set; }
        
        /// <summary>
        /// Current operational state of the elevator (Idle or Moving).
        /// </summary>
        public ElevatorState State { get; private set; } = ElevatorState.Idle;

        /// <summary>
        /// Current movement direction of the elevator (None, Up, Down).
        /// </summary>
        public DirectionState DirectionState { get; private set; } = DirectionState.None;

        /// <summary>
        /// Simulates moving the elevator to a target floor, respecting speed and direction with the maximum number of floors the elevator can move to.
        /// </summary>
        /// <param name="targetFloor">The floor to which the elevator should move.</param>
        /// /// <param name="maxFloors">The maximum number of floors the elevator can move.</param>
        public async Task MoveToFloor(int targetFloor, int maxFloors)
        {
            if (targetFloor < 0 || targetFloor > maxFloors)
            {
                Console.WriteLine($"{Name} cannot move to floor {targetFloor} (out of range).");
                return;
            }

            DirectionState = targetFloor > CurrentFloor ? DirectionState.Up :
                               targetFloor < CurrentFloor ? DirectionState.Down : DirectionState.None; ;

            State = ElevatorState.Moving;

            Console.WriteLine($"{Name} moving {DirectionState} from floor {CurrentFloor} to {targetFloor}.");

            int floorsToTravel = Math.Abs(CurrentFloor - targetFloor);
            int delayMs = (int)(floorsToTravel / SpeedPerSecond * 1000);

            await Task.Delay(delayMs); // Simulate movement

            CurrentFloor = targetFloor;
            Console.WriteLine($"{Name} arrived at floor {CurrentFloor}.");
            
            State = ElevatorState.Idle;
            DirectionState = DirectionState.None;
        }
    }
}
