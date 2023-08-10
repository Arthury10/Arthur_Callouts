using LSPD_First_Response.Engine.Scripting.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net;
using Rage;

namespace ArthurCallouts.Server.Modules
{
    public class VehicleInformationHandler
    {
        private readonly Random _Random = new Random();

        public object HandleGet(HttpListenerRequest request)
        {
            string licensePlate = request.Url.Segments.Last();

            List<Vehicle> vehicles = World.GetAllVehicles().ToList();
            List<Ped> peds = World.GetAllPeds().ToList();

            Game.Console.Print("Buscando veículo com placa " + licensePlate);
            Game.Console.Print("Proprietario " + peds.Count);

            Ped ped = peds[_Random.Next(0, peds.Count + 1)];


            Persona persona = LSPD_First_Response.Mod.API.Functions.GetPersonaForPed(ped);
            Vehicle vehicle = vehicles.Find(v => v.LicensePlate == licensePlate);

            LSPD_First_Response.Mod.API.Functions.SetVehicleOwnerName(vehicles.Find(v => v.LicensePlate == licensePlate), persona.FullName);

            DateTime dateTime = new DateTime(_Random.Next(1990, 2000), _Random.Next(1, 12), _Random.Next(1, 28));
            
            persona.Birthday = dateTime;

            persona.WantedInformation.LastSeenPosition = ped.Position;

            //vamos setar um tempo de 0 a 10 dias para a ultima vez que foi visto
            DateTime dateTimeLastSeen = DateTime.Now.AddDays(_Random.Next(0, 10));
            persona.WantedInformation.LastSeenUtc = dateTimeLastSeen;


            if (persona.Wanted)
            {
                if (_Random.NextDouble() < 0.5)
                {
                    ped.Inventory.GiveNewWeapon(new WeaponAsset("WEAPON_PISTOL"), 100, false);
                }
            }


            if (vehicles != null)
            {
                return new
                {
                    personaData = persona,
                    vehicleModel = vehicle.Model.Name,
                    primaryColor = vehicle.PrimaryColor,
                    secondaryColor = vehicle.SecondaryColor,
                    licensePlate = vehicle.LicensePlate,
                    ownerName = LSPD_First_Response.Mod.API.Functions.GetVehicleOwnerName(vehicle),
                    isStolen = vehicle.IsStolen,
                    insuranceStatus = StopThePed.API.Functions.getVehicleInsuranceStatus(vehicle),
                    registrationStatus = StopThePed.API.Functions.getVehicleRegistrationStatus(vehicle),
                    sreetWasLastSeen = World.GetStreetName(ped.Position),
                };
            }

            return new
            {
                message = "Placa não encontrada.",
                licensePlate = "NADA",
                name = "NADA"
            };
        }

        public object HandlePost(HttpListenerRequest request)
        {
            // Lógica para lidar com a solicitação POST de veículos
            // ...
            return new { message = "POST de veículos" };
        }

        public object HandlePut(HttpListenerRequest request)
        {
            // Lógica para lidar com a solicitação PUT de veículos
            // ...
            return new { message = "PUT de veículos" };
        }

        // Métodos adicionais para POST, PUT, DELETE...
    }
}