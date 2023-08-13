using ArthurCallouts.Server.DB.Repository;
using System.IO;

namespace ArthurCallouts.Server.Db
{
    public class MainDBContext
    {
        public UserRepository UserRepository { get; private set; }
        public PedRepository PedRepository { get; private set; }
        public VehicleRepository VehicleRepository { get; private set; }
        public FineRepository FineRepository { get; private set; }
        public WantedRepository WantedRepository { get; private set; }

        public MainDBContext()
        {
            string directoryPath = "Plugins/LSPDFR/ArthurCallouts";
            Directory.CreateDirectory(directoryPath); // Cria o diretório se ele não existir
            UserRepository = new UserRepository(directoryPath);
            PedRepository = new PedRepository(directoryPath);
            VehicleRepository = new VehicleRepository(directoryPath);
            FineRepository = new FineRepository(directoryPath);
            WantedRepository = new WantedRepository(directoryPath);
        }
    }
}
