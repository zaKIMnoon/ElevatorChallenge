using ElevatorChallengeLib.Models.Enums;

namespace ElevatorChallengeLib.Models
{
    public class FloorRequest
    {
        public int Floor { get; set; }
        public int PeopleCount { get; set; }
        public DirectionState RequestedDirection { get; set; }
    }
}
