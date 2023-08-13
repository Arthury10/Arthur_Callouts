using ArthurCallouts.Services;
using System;
using System.IO;
using ArthurCallouts.Server.DB.Models;

namespace ArthurCallouts.Server.DB.Repository
{
    public class FineRepository
    {
        private SerializeService _SerializeService = new SerializeService();
        private readonly string _DirectoryPath;

        private LoggerService _LoggerService = new LoggerService();

        public FineRepository(string directoryPath)
        {
            _DirectoryPath = directoryPath;
        }

        public void SaveFine(FineModel fine)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/fines/fine-{fine.FineId}.dat");
            SerializeToFile(filePath, fine);
            _LoggerService.Info($"Fine {fine.FineName} saved.");
        }

        public FineModel GetFine(Guid fineId)
        {
            _LoggerService.Info($"Loading fine {fineId}...");

            if (!_SerializeService.FileExists(Path.Combine(_DirectoryPath, $"db/fines/fine-{fineId}.dat")))
            {
                _LoggerService.Info($"Fine {fineId} not found.");
                return null;
            }

            string filePath = Path.Combine(_DirectoryPath, $"db/fines/fine-{fineId}.dat");
            _LoggerService.Info($"Fine {fineId} loaded.");
            return DeserializeFromFile<FineModel>(filePath);
        }

        public void UpdateFine(FineModel fine)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/fines/fine-{fine.FineId}.dat");
            SerializeToFile(filePath, fine);
            _LoggerService.Info($"Fine {fine.FineName} updated.");
        }

        public void DeleteFine(FineModel fine)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/fines/fine-{fine.FineId}.dat");
            File.Delete(filePath);
            _LoggerService.Info($"Fine {fine.FineName} deleted.");
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
