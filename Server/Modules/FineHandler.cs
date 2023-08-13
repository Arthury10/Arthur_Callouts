using ArthurCallouts.Server.Db;
using ArthurCallouts.Server.DB.Models;
using ArthurCallouts.Services;
using LSPD_First_Response.Engine.Scripting.Entities;
using Newtonsoft.Json;
using Rage;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;

namespace ArthurCallouts.Server.Modules
{

    public class FineHandler
    {
        private Vehicle _Vehicle;
        private Ped _Ped;
        private Persona _Persona;

        private VehicleModel _VehicleInformationData;

        private PedModel _PedInformationData;

        private FineModel _FineInformationData;

        private LoggerService _Logger = new LoggerService();

        private MainDBContext _MainDBContext = new MainDBContext();

        private RebuildPedPersonService _RebuildPedPersonService = new RebuildPedPersonService();

        private PedResult _PedResult = new PedResult();

        public object HandleGet(HttpListenerRequest request)
        {
              return new {message = "Hello"};
        }

        public object HandlePost(HttpListenerRequest request)
        {
            // Ler o corpo da solicitação
            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                string requestBody = reader.ReadToEnd();
                var fine = JsonConvert.DeserializeObject<FineModel>(requestBody);

                _Logger.Info("Fine post body value" + fine.VehicleLicensePlate);
                _Logger.Info("Fine post body value" + fine.PedId);
                _Logger.Info("Fine post body value" + fine.Amount);
                _Logger.Info("Fine post body name" + fine.FineName);
                _Logger.Info("Fine post body description" + fine.Description);
                _Logger.Info("Fine post body reason" + fine.Reason);
                _Logger.Info("Fine post body date" + fine.Date);
                _Logger.Info("Fine post body status" + fine.FineStatus);
                _Logger.Info("Fine post body removeLicense" + fine.PedLicenseStatus);
                _Logger.Info("Fine post body removeInsurance" + fine.VehicleInsuranceStatus);
                _Logger.Info("Fine post body removeLicenseStatus" + fine.VehicleLicenseStatus);



                _PedInformationData = _MainDBContext.PedRepository.GetPed(fine.PedId);
                _VehicleInformationData = _MainDBContext.VehicleRepository.GetVehicle(fine.VehicleLicensePlate);

                if (_PedInformationData == null)
                {
                    _Logger.Error("Ped not found");
                    return new { message = "Ped not found" };
                }

                if (_VehicleInformationData == null)
                {
                    _Logger.Error("Vehicle not found");
                    return new { message = "Vehicle not found" };
                }

                List<Vehicle> vehicles = World.GetAllVehicles().ToList();
                _Vehicle = vehicles.Find(x => x.LicensePlate == _VehicleInformationData.LicensePlate);
                Game.Console.Print("Vehicle found: " + _Vehicle.LicensePlate);


                _PedResult = _RebuildPedPersonService.ReconstrucaoPed(_PedInformationData);

                _Persona = _PedResult.Persona;
                _Ped = _PedResult.Ped;

                Game.Console.Print("Ped: " + _PedResult.Ped.Model.Name);
                Game.Console.Print("Persona: " + _PedResult.Persona.FullName);

                _Persona.Citations += 1;

                _PedInformationData.Citations += 1;

                _Persona.ELicenseState = fine.PedLicenseStatus;


                StopThePed.API.Functions.setVehicleInsuranceStatus(_Vehicle, 0);
                StopThePed.API.Functions.setVehicleRegistrationStatus(_Vehicle, 0);

                LSPD_First_Response.Mod.API.Functions.SetVehicleOwnerName(_Vehicle, _Persona.FullName);

                LSPD_First_Response.Mod.API.Functions.GetPersonaForPed(_Ped).Citations += 1;

                _MainDBContext.PedRepository.UpdatePed(_PedInformationData);

                _MainDBContext.VehicleRepository.UpdateVehicle(_VehicleInformationData);

                _FineInformationData = new FineModel
                {
                    FineName = "Vehicle",
                    Description = fine.Description,
                    VehicleLicensePlate = fine.VehicleLicensePlate,
                    PedId = fine.PedId,
                    Amount = fine.Amount,
                    Date = fine.Date,
                    Location = _VehicleInformationData.Position,
                    Reason = fine.Reason,
                    FineStatus = "PENDING",
                    PedLicenseStatus = fine.PedLicenseStatus,
                    VehicleInsuranceStatus = fine.VehicleInsuranceStatus,
                    VehicleLicenseStatus = fine.VehicleLicenseStatus
                };

                _MainDBContext.FineRepository.SaveFine(_FineInformationData);

                return _FineInformationData;
            }
        }
    }
}
