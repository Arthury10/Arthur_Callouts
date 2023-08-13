using ArthurCallouts.Server.DB.Models;
using ArthurCallouts.Services;
using System;
using System.IO;

namespace ArthurCallouts.Server.DB.Repository
{
    public class PedRepository
    {
        private LoggerService _LoggerService = new LoggerService();
        private SerializeService _SerializeService = new SerializeService();


        private readonly string _DirectoryPath;


        public PedRepository(string directoryPath)
        {
            _DirectoryPath = directoryPath;
        }

        public void SavePed(PedModel ped)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/peds/ped-{ped.PedId}.dat");
            SerializeToFile(filePath, ped);
            _LoggerService.Info($"Ped {ped.PedId} saved.");
        }

        public PedModel GetPed(Guid pedId)
        {
            _LoggerService.Info($"Loading ped {pedId}...");

            if (!_SerializeService.FileExists(Path.Combine(_DirectoryPath, $"db/peds/ped-{pedId}.dat")))
            {
                _LoggerService.Info($"Ped {pedId} not found.");
                return null;
            }

            string filePath = Path.Combine(_DirectoryPath, $"db/peds/ped-{pedId}.dat");
            _LoggerService.Info($"Ped {pedId} loaded.");
            return DeserializeFromFile<PedModel>(filePath);
        }

        public void UpdatePed(PedModel ped)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/peds/ped-{ped.PedId}.dat");
            SerializeToFile(filePath, ped);
            _LoggerService.Info($"Ped {ped.PedId} updated.");
        }

        public void DeleteVehicle(PedModel ped)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/peds/ped-{ped.PedId}.dat");
            File.Delete(filePath);
            _LoggerService.Info($"Ped {ped.PedId} deleted.");
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
