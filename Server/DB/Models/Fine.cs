using LSPD_First_Response.Engine.Scripting.Entities;
using Rage;
using System;

namespace ArthurCallouts.Server.DB.Models
{
    [Serializable] // Necessário para a serialização binária
    public class FineModel
    {
        Guid newId = Guid.NewGuid();
        public Guid FineId { get { return newId; } }
        public string FineName { get; set; }
        public int Amount { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Vector3 Location { get; set; }
        public string FineStatus { get; set; }
        public Guid PedId { get; set; }
        public string VehicleLicensePlate { get; set; }
        public int VehicleInsuranceStatus { get; set; }
        public int VehicleLicenseStatus { get; set; }
        public ELicenseState PedLicenseStatus { get; set; }
    }

}
