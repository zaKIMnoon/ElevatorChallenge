namespace ElevatorChallengeLib.Models.Config
{
    /// <summary>
    /// The Building Configuration Model used from json file.
    /// </summary>
    public class BuildingConfig
    {
        /// <summary>
        /// Building Name from the json file
        /// </summary>
        public string BuildingName { get; set; }
        
        /// <summary>
        /// Building Number of Floors from the json file
        /// </summary>
        public int NumberOfFloors { get; set; }
        
        /// <summary>
        /// Building List of Elevators from the json file
        /// </summary>
        public List<Elevator> ElevatorList { get; set; }
    }
}
