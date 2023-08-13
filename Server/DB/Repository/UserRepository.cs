using ArthurCallouts.Server.DB.Models;
using ArthurCallouts.Services;
using System.IO;

namespace ArthurCallouts.Server.Db
{
    public class UserRepository
    {
        private SerializeService _SerializeService = new SerializeService();
        private readonly string _DirectoryPath;

        private LoggerService _LoggerService = new LoggerService();

        public UserRepository(string directoryPath)
        {
            _DirectoryPath = directoryPath;
        }

        public void SaveUser(UserModel user)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/user-{user.UserId}.dat");
            SerializeToFile(filePath, user);
            _LoggerService.Info($"Usuário {user.Name} salvo em {filePath}");
        }

        public UserModel GetUser(int userId)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/user-{userId}.dat");
            _LoggerService.Info($"Carregando usuário {userId} de {filePath}");
            return DeserializeFromFile<UserModel>(filePath);
        }

        public void UpdateUser(UserModel user)
        {
            string filePath = Path.Combine(_DirectoryPath, $"db/user-{user.UserId}.dat");
            SerializeToFile(filePath, user);
            _LoggerService.Info($"Usuário {user.Name} atualizado em {filePath}");
        }

        // Outros métodos CRUD aqui...


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
