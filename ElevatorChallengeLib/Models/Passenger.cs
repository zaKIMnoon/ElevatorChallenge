namespace ElevatorChallengeLib.Models
{
    public class Passenger
    {
        public int RequestedFloor { get; set; }
        public int CurrentFloor { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime? PickupTime { get; set; }
        public DateTime? DropOffTime { get; set; }
    }
}
