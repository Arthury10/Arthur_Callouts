using ArthurCallouts.Server.Db;
using ArthurCallouts.Server.DB.Models;
using LSPD_First_Response.Engine.Scripting.Entities;
using Rage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArthurCallouts.Services
{
    public class CreateVehicleService
    {
        public Random _Random = new Random();
        private LoggerService _Logger = new LoggerService();
        private MainDBContext _DbContext = new MainDBContext();

        private Vehicle _Vehicle;
        private VehicleModel _VehicleModel;
        private PedModel _PedModel;

        public Vehicle CreateVehicle(string model = null)
        {
            // Se o modelo for fornecido, use esse modelo, caso contrário, gere aleatoriamente.
            if (string.IsNullOrEmpty(model))
            {
                _Vehicle = new Vehicle(Vector3.Zero);
                _Logger.Info("Veículo aleatório criado");
                return _Vehicle;
            }
            else
            {
                _Vehicle = new Vehicle(model, Vector3.Zero);
                _Logger.Info("Veículo " + _Vehicle.Model.Name + " criado");
                return _Vehicle;
            }
        }

        public VehicleModel CreateVehicleModel(string licensePlate, Guid ownerId)
        {
             List <Vehicle> vehicles = World.GetAllVehicles().ToList();
            _Logger.Info("Veículos encontrados: " + vehicles.Count);

            _Vehicle = vehicles.Find(v => v.LicensePlate == licensePlate);
            _Logger.Info("Veículo encontrado: " + _Vehicle.Model.Name);

            
            _PedModel = _DbContext.PedRepository.GetPed(ownerId);
            _Logger.Info("PedModel encontrado");

            if (_Vehicle == null)
            {
                Game.DisplayNotification("Não foi possível encontrar o veículo com a placa " + licensePlate);
                _Logger.Error("Não foi possível encontrar o veículo com a placa " + licensePlate);
                return null;
            }

            _VehicleModel = new VehicleModel {
                LicensePlate = _Vehicle.LicensePlate,
                LicensePlateType = _Vehicle.LicensePlateType.ToString(),
                OwnerName = LSPD_First_Response.Mod.API.Functions.GetVehicleOwnerName(_Vehicle),
                IsStolen = _Vehicle.IsStolen,
                InsurenceStatus = StopThePed.API.Functions.getVehicleInsuranceStatus(_Vehicle).ToString(),
                RegistrationStatus = StopThePed.API.Functions.getVehicleRegistrationStatus(_Vehicle).ToString(),
                ModelName = _Vehicle.Model.Name,
                PrimaryColor = _Vehicle.PrimaryColor,
                SecondaryColor = _Vehicle.SecondaryColor,
                Position = _Vehicle.Position,
                AbovePosition = _Vehicle.AbovePosition,
                BelowPosition = _Vehicle.BelowPosition,
                ForwardVector = _Vehicle.ForwardVector,
                IsWanted = _PedModel.IsWanted,
                OwnerModelName = _PedModel.ModelName,
                OwenerId = ownerId,
            };

            _Logger.Info("VeículoModel criado");

            _DbContext.VehicleRepository.SaveVehicle(_VehicleModel);
            _Logger.Info("VeículoModel salvo no banco de dados");

            _PedModel.Vehicles.Add(_VehicleModel);
            _Logger.Info("Veículo adicionado ao PedModel");

            _DbContext.PedRepository.UpdatePed(_PedModel);
            _Logger.Info("PedModel atualizado no banco de dados");

            return _VehicleModel;
        }
    }
}
