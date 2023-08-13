using Rage;
using System;

namespace ArthurCallouts.Server.DB.Models
{
    [Serializable] // Necessário para a serialização binária
    public class VehicleModel
    {
        public Guid OwenerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerModelName { get; set; }
        public string LicensePlate { get; set; }
        public string LicensePlateType { get; set; }
        public string ModelName { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 AbovePosition { get; set; }
        public Vector3 BelowPosition { get; set; }
        public Vector3 ForwardVector { get; set; }
        public System.Drawing.Color PrimaryColor { get; set; }
        public System.Drawing.Color SecondaryColor { get; set; }
        public bool IsStolen { get; set; }
        public bool IsWanted { get; set; }
        public string InsurenceStatus { get; set; }
        public string RegistrationStatus { get; set; }
        public Guid WantedId { get; set; }

    }
}
