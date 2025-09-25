using ElevatorChallengeLib.Models.Enums;

namespace ElevatorChallengeLib.Models
{
    /// <summary>
    /// Represents a request made from a floor, including the number of people waiting.
    /// </summary>
    public class FloorRequest
    {
        /// <summary>
        /// The floor number where the request was made.
        /// </summary>
        public int Floor { get; set; }

        /// <summary>
        /// Number of people waiting for the elevator.
        /// </summary>
        public int PeopleCount { get; set; }

        /// <summary>
        /// Direction of travel requested (Up or Down).
        /// </summary>
        public DirectionState RequestedDirection { get; set; }
    }
}
