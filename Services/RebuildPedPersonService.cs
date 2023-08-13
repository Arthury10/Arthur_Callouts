using ArthurCallouts.Server.DB.Models;
using System.Collections.Generic;
using System.Linq;
using Rage;
using LSPD_First_Response.Engine.Scripting.Entities;

namespace ArthurCallouts.Services
{
    public class PedResult
    {
        public Ped Ped { get; set; }
        public Persona Persona { get; set; }
    }

    public class RebuildPedPersonService
    {
        private LoggerService _Logger = new LoggerService();
        private List<Ped> _PedsWithSameModel;
        private Ped _Ped;

        private Persona _Persona;

        private Vector3 _PlayerPosition;

        private PedResult _Result;

        public PedResult ReconstrucaoPed(PedModel pedModel)
        {
            _PlayerPosition = Game.LocalPlayer.Character.Position;

            List<Ped> peds = World.GetAllPeds().ToList();
            _Logger.Info("Peds encontrados: " + peds.Count);

            _PedsWithSameModel = peds.FindAll(x => x.Model.Name == pedModel.ModelName);
            _Logger.Info("Peds PedsWithSameModel encontrados: " + _PedsWithSameModel.Count);


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
            _Logger.Info("Criando nova persona: " + _Persona.FullName);

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

            _Logger.Info("Reconstrução do ped concluída");
            return _Result;
        }
    }
}
