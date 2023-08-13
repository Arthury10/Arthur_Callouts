using ArthurCallouts.Server.DB.Models;
using ArthurCallouts.Server.Db;
using System;
using System.Collections.Generic;
using System.Linq;

using Rage;
using LSPD_First_Response.Engine.Scripting.Entities;

namespace ArthurCallouts.Services
{
    public class CreatePersonaService
    {
        private CreatePedService _PedService = new CreatePedService();
        private LoggerService _Logger = new LoggerService();
        private MainDBContext _DbContext = new MainDBContext();
        private Random _Random = new Random();
        private Ped _Ped;
        private Vector3 _PositionPedAround;
        private Persona _Persona;

        private DateTime GenerateRandomBirthday()
        {
            int age = _Random.Next(20, 41);
            DateTime birthDate = DateTime.Today.AddYears(-age);
            return birthDate;
        }

        public PedModel CreatePerson(Vector3 position, string model = null, float heading = 0)
        {
            PedModel _PedInformationData;

            if (model == null && position == new Vector3(0, 0, 0))
            {

                List<Ped> peds = World.GetAllPeds().ToList();
                _Ped = peds[_Random.Next(0, peds.Count + 1)];
                _PositionPedAround = _Ped.Position.Around(1000f);
                _Logger.Info("Ped aleatorio encontrado");
            }
            else
            {
                _Ped = _PedService.CreatePed(model, position, heading);
                _PositionPedAround = World.GetNextPositionOnStreet(position.Around(1000f));
                _Logger.Info("Ped criado");
            }

            _Persona = LSPD_First_Response.Mod.API.Functions.GetPersonaForPed(_Ped);
            _Logger.Info("_Persona criado");

            string StreetName = World.GetStreetName(_PositionPedAround);
            _Logger.Info("_Persona StreetName name" + StreetName);

            _Persona.Birthday = GenerateRandomBirthday();

            _PedInformationData = new PedModel
            {
                BirthDay = _Persona.Birthday,
                Citations = _Persona.Citations,
                ForeName = _Persona.Forename,
                Gender = _Persona.Gender,
                Name = _Persona.FullName,
                IsWanted = _Persona.Wanted,
                IsAgent = _Persona.RuntimeInfo.IsAgent,
                IsCop = _Persona.RuntimeInfo.IsCop,
                LicenseState = _Persona.ELicenseState,
                ModelAge = _Persona.ModelAge.ToString(),
                ModelName = _Ped.Model.Name,
                CarriesEvidence = _Persona.WantedInformation.CarriesEvidence,
                EscapedInVehicle = _Persona.WantedInformation.EscapedInVehicle,
                LastSeenLocation = StreetName,
                LastSeenPosition = _PositionPedAround,
                SurName = _Persona.Surname,
            };
            _Logger.Info("PedModel criado");

            _DbContext.PedRepository.SavePed(_PedInformationData);
            _Logger.Info("PedModel Salvo");

            return _PedInformationData;
        }
    }
}
