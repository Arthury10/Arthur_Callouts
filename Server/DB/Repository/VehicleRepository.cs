using ArthurCallouts.Server.DB.Models;
using ArthurCallouts.Services;
using System.IO;

namespace ArthurCallouts.Server.DB.Repository
{
    public class VehicleRepository
    {
        private string _DirectoryPath;
        private SerializeService _SerializeService = new SerializeService();

        private LoggerService _LoggerService = new LoggerService();

        public VehicleRepository(string directoryPath)
        {
            _DirectoryPath = directoryPath;
        }

        public void SaveVehicle(VehicleModel vehicle)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/vehicles/vehicle-{vehicle.LicensePlate}.dat");
            SerializeToFile(filePath, vehicle);
            _LoggerService.Info($"Vehicle {vehicle.LicensePlate} saved.");
        }

        public VehicleModel GetVehicle(string vehicleLicensePlate)
        {
            _LoggerService.Info($"Loading vehicle {vehicleLicensePlate}...");

            if (!_SerializeService.FileExists(Path.Combine(_DirectoryPath, $"db/vehicles/vehicle-{vehicleLicensePlate}.dat")))
            {
                _LoggerService.Info($"Vehicle {vehicleLicensePlate} not found.");
                return null;
            }

            string filePath = Path.Combine(_DirectoryPath, $"db/vehicles/vehicle-{vehicleLicensePlate}.dat");
            _LoggerService.Info($"Vehicle {vehicleLicensePlate} loaded.");
            return DeserializeFromFile<VehicleModel>(filePath);
        }

        public void UpdateVehicle(VehicleModel vehicle)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/vehicles/vehicle-{vehicle.LicensePlate}.dat");
            SerializeToFile(filePath, vehicle);
            _LoggerService.Info($"Vehicle {vehicle.LicensePlate} updated.");
        }

        public void DeleteVehicle(VehicleModel vehicle)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/vehicles/vehicle-{vehicle.LicensePlate}.dat");
            File.Delete(filePath);
            _LoggerService.Info($"Vehicle {vehicle.LicensePlate} deleted.");
        }

        private void SerializeToFile<T>(string filePath, T data)
        {
            _SerializeService.SerializeToFile(filePath, data);
        }

        private T DeserializeFromFile<T>(string filePath)
        {
            return _SerializeService.DeserializeFromFile<T>(filePath);
        }

    }
}
