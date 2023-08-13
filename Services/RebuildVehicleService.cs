using ArthurCallouts.Server.DB.Models;
using System.Collections.Generic;
using System.Linq;
using Rage;
using LSPD_First_Response.Engine.Scripting.Entities;

namespace ArthurCallouts.Services
{

    public class RebuildVehicleService
    {
        private LoggerService _Logger = new LoggerService();
        private List<Vehicle> _VehiclesWithSameModel;
        private Vector3 _PlayerPosition;

        public PedResult ReconstrucaoPed(VehicleModel vehicleModel)
        {
            /*
            _PlayerPosition = Game.LocalPlayer.Character.Position;

            List<Vehicle> vehicles = World.GetAllVehicles().ToList();
            _Logger.Info("Vehicles encontrados: " + vehicles.Count);

            _VehiclesWithSameModel = vehicles.FindAll(x => x.Model.Name == vehicleModel.ModelName);

            _Logger.Info("Vehicle PedsWithSameModel encontrados: " + _VehiclesWithSameModel.Count);


            if (_PedsWithSameModel.Count > 0)
            {
                //precisamos pegar o ped mais próximo do player 
                _Ped = _PedsWithSameModel.OrderBy(x => x.DistanceTo(_PlayerPosition)).First();
                _Logger.Info("Ped mais próximo: " + _Ped.Model.Name);
            }
            else
            {
                //verificar se é necessario criar um novo ped
                _Ped = new Ped(pedModel.ModelName, pedModel.LastSeenPosition, 0);
                _Logger.Info("Criando novo ped: " + _Ped.Model.Name);
            }

            _Persona = new Persona(pedModel.ForeName, pedModel.SurName, pedModel.Gender)
            {
                Birthday = pedModel.BirthDay,
                ELicenseState = pedModel.LicenseState,
                Citations = pedModel.Citations,
                Wanted = pedModel.IsWanted
            };

            _Persona.WantedInformation.CarriesEvidence = pedModel.CarriesEvidence;
            _Persona.WantedInformation.EscapedInVehicle = pedModel.EscapedInVehicle;
            _Persona.WantedInformation.LastSeenPosition = pedModel.LastSeenPosition;
            _Persona.WantedInformation.LastSeenUtc = pedModel.LastSeenUtc;
            _Persona.TimesStopped = 0;
            _Persona.RuntimeInfo.IsAgent = pedModel.IsAgent;
            _Persona.RuntimeInfo.IsCop = pedModel.IsCop;
            //_Persona.WantedInformation.CanBeSearchedInManhunt = "NÃO SEI";
            //_Persona.WantedInformation.IsWantedInManhunt = true;

            LSPD_First_Response.Mod.API.Functions.SetPersonaForPed(_Ped, _Persona);

            _Result = new PedResult
            {
                Ped = _Ped,
                Persona = _Persona
            };

            return _Result;
            */

            return null;
        }
    }
}
