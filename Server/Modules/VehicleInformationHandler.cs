using System.Collections.Generic;
using System.Linq;
using System;
using System.Net;
using Rage;
using StopThePed.API;
using ArthurCallouts.Server.Db;
using ArthurCallouts.Server.DB.Models;
using ArthurCallouts.Services;

//vamoc criar o modelo de dados para o veículo

namespace ArthurCallouts.Server.Modules
{
    public class VehicleInformationHandler
    {
        public VehicleModel _vehicleInformationData;

        public PedModel _pedInformationData;

        private LoggerService _Logger = new LoggerService();

        public CreatePersonaService _CreatePersonaService = new CreatePersonaService();
        public CreateVehicleService _CreateVehicleModel = new CreateVehicleService();

        public MainDBContext _MainDBContext = new MainDBContext();


        public object HandleGet(HttpListenerRequest request)
        {
            string licensePlate = request.Url.Segments.Last();
            
            Game.DisplayNotification("Buscando veículo com placa " + licensePlate);



            if (_vehicleInformationData != null)
            {
                if (licensePlate == _vehicleInformationData.LicensePlate)
                {
                    return _vehicleInformationData;
                } else
                {
                    _vehicleInformationData = _MainDBContext.VehicleRepository.GetVehicle(licensePlate);

                    if (_vehicleInformationData != null)
                    {
                        return _vehicleInformationData;
                    }
                }
            }

            Vector3 vector3 = new Vector3(0, 0, 0);

            _pedInformationData = _CreatePersonaService.CreatePerson(vector3);

            _vehicleInformationData = _CreateVehicleModel.CreateVehicleModel(licensePlate, _pedInformationData.PedId);

            if (_vehicleInformationData != null)
            {
                return _vehicleInformationData;
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