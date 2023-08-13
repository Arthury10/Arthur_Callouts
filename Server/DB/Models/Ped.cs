using LSPD_First_Response;
using LSPD_First_Response.Engine.Scripting.Entities;
using Rage;
using System;
using System.Collections.Generic;

namespace ArthurCallouts.Server.DB.Models
{


    [Serializable] // Necessário para a serialização binária
    public class PedModel
    {
        Guid newId = Guid.NewGuid();
        public Guid PedId { get { return newId; } }
        public string Name { get; set; }
        public string ForeName { get; set; }
        public string SurName { get; set; }
        public DateTime BirthDay { get; set; }
        public int Citations { get; set; }
        public ELicenseState LicenseState { get; set; }
        public Gender Gender { get; set; }
        public string ModelName { get; set; }
        public string ModelAge { get; set; }
        public bool IsAgent { get; set; }
        public bool IsCop { get; set; }
        public bool IsWanted { get; set; }
        public Vector3 LastSeenPosition { get; set; }
        public string LastSeenLocation { get; set; }
        public DateTime LastSeenUtc { get; set; }
        public bool CarriesEvidence { get; set; }
        public bool EscapedInVehicle { get; set; }
        public Guid WantedId { get; set; }
        public List<VehicleModel> Vehicles { get; set; } = new List<VehicleModel>(); // Lista de veículos associados

    }
}
