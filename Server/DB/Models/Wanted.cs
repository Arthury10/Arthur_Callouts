using System;

namespace ArthurCallouts.Server.DB.Models
{
    [Serializable]
    public class WantedModel
    {
        Guid newId = Guid.NewGuid();

        public Guid WantedId { get { return newId; } }
        public Guid PedId { get; set; }
        public string VehicleId { get; set; }
        public string PedName { get; set; }
        public string VehicleModelName { get; set; }
        public string WantedLevel { get; set; }
        public string WantedReason { get; set; }
        public string WantedDescription { get; set; }
        public string WantedLastSeen { get; set; }
        public string WantedLastSeenLocation { get; set; }
    }
}
