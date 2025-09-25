using ElevatorChallengeLib.Models.Enums;

namespace ElevatorChallengeLib.Models
{
    public class Elevator
    {
        public string Name { get; set; }
        public decimal SpeedPerSecond { get; set; }
        public int CurrentFloor { get; set; }
        public int Capacity { get; set; }
        public ElevatorState State { get; private set; } = ElevatorState.Idle;
        public DirectionState DirectionState { get; private set; } = DirectionState.None;
        private int MaxFloors;

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
