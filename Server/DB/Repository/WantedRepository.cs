using ArthurCallouts.Services;
using System;
using System.IO;
using ArthurCallouts.Server.DB.Models;

namespace ArthurCallouts.Server.DB.Repository
{
    public class WantedRepository
    {
        private SerializeService _SerializeService = new SerializeService();
        private readonly string _DirectoryPath;

        private LoggerService _LoggerService = new LoggerService();

        public WantedRepository(string directoryPath)
        {
            _DirectoryPath = directoryPath;
        }

        public void SaveWanted(WantedModel wanted)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/wanteds/wanted-{wanted.WantedId}.dat");
            SerializeToFile(filePath, wanted);
            _LoggerService.Info($"Wanted {wanted.PedName} saved.");
        }

        public WantedModel GetWanted(Guid wantedId)
        {
            _LoggerService.Info($"Loading ped {wantedId}...");

            if (!_SerializeService.FileExists(Path.Combine(_DirectoryPath, $"db/wanteds/wanted-{wantedId}.dat")))
            {
                _LoggerService.Info($"Ped {wantedId} not found.");
                return null;
            }

            string filePath = Path.Combine(_DirectoryPath, $"db/wanteds/wanted-{wantedId}.dat");
            _LoggerService.Info($"Wanted {wantedId} loaded.");
            return DeserializeFromFile<WantedModel>(filePath);
        }

        public void UpdateWanted(WantedModel wanted)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/wanteds/wanted-{wanted.WantedId}.dat");
            SerializeToFile(filePath, wanted);
            _LoggerService.Info($"Wanted {wanted.PedName} updated.");
        }

        public void DeleteWanted(WantedModel wanted)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/wanteds/wanted-{wanted.WantedId}.dat");
            File.Delete(filePath);
            _LoggerService.Info($"Wanted {wanted.PedName} deleted.");
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
