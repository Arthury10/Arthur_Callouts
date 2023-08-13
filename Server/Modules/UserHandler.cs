using ArthurCallouts.Server.DB.Models;
using ArthurCallouts.Server.Db;
using Rage;
using System.Net;

namespace ArthurCallouts.Server.Modules
{
    public class UserHandler
    {
        public UserModel HandleGet(HttpListenerRequest request)
        {
            Player player = Game.LocalPlayer; // Obter o jogador local

            MainDBContext dbContext = new MainDBContext();

            UserModel user = new UserModel
            {
                Name = player.Name ?? "ERRO",
                Health = player.Character.Health,
                Armor = player.Character.Armor,
                Money = player.Character.Money,
                IsMale = player.Character.IsMale,
                Position = player.Character.Position,
                AbovePosition = player.Character.AbovePosition,
                BelowPosition = player.Character.BelowPosition,
                ForwardVector = player.Character.ForwardVector,
                Velocity = player.Character.Velocity,
                Speed = player.Character.Speed,
                IsDead = player.Character.IsDead,
                Model = player.Character.Model.Name,
                UserId = player.Id,
                Office = "LSPD",
                Experience = 0,
                ExperienceToNextLevel = 100,
                Level = 0,
            };

            dbContext.UserRepository.SaveUser(user); // Salva o usuário no banco de dados
            Game.Console.Print($"Usuário {user.Name} salvo com sucesso!"); // Imprime uma mensagem no console do RagePluginHook

            // Lógica para lidar com a solicitação GET de usuários

            Game.Console.Print(dbContext.UserRepository.GetUser(player.Id).Name);


            return user;
        }

        public object HandlePost(HttpListenerRequest request)
        {
            // Lógica para lidar com a solicitação POST de usuários
            // ...
            return new { message = "POST de usuários" };
        }

        public object HandlePut(HttpListenerRequest request)
        {
            // Lógica para lidar com a solicitação PUT de usuários
            // ...
            return new { message = "PUT de usuários" };
        }

        public object HandleDelete(HttpListenerRequest request)
        {
            // Lógica para lidar com a solicitação DELETE de usuários
            // ...
            return new { message = "DELETE de usuários" };
        }

        // Você pode adicionar métodos para POST, PUT, DELETE aqui também
    }
}