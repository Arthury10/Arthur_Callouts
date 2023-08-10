using Rage;
using System.Net;

namespace ArthurCallouts.Server.Modules
{
    public class UserHandler
    {
        public object HandleGet(HttpListenerRequest request)
        {
            Player player = Game.LocalPlayer; // Obter o jogador local

            Game.Console.Print("Player: " + player.Name);
            Game.Console.Print("Health: " + player.Character.Health);
            Game.Console.Print("Armor: " + player.Character.Armor);
            Game.Console.Print("Money: " + player.Character.Money);
            Game.Console.Print("IsMale: " + player.Character.IsMale);
            Game.Console.Print("Iventory: " + player.Id);
            Game.Console.Print("Position: " + player.Character.Position);
            Game.Console.Print("Rotation: " + player.Character.Rotation);
            Game.Console.Print("AbovePosition: " + player.Character.AbovePosition);
            Game.Console.Print("BelowPosition: " + player.Character.BelowPosition);
            Game.Console.Print("ForwardVector: " + player.Character.ForwardVector);
            Game.Console.Print("Velocity: " + player.Character.Velocity);
            Game.Console.Print("Speed: " + player.Character.Speed);
            Game.Console.Print("IsDead: " + player.Character.IsDead);
            Game.Console.Print("Model: " + player.Character.Model.Name);



            return new 
            { 
                name =  player.Name ?? "ERRO", 
                health = player.Character.Health, 
                armor = player.Character.Armor,
                money = player.Character.Money ,
                isMale = player.Character.IsMale,
                iventory = player.Id,
                position = player.Character.Position,
                abovePosition = player.Character.AbovePosition,
                belowPosition = player.Character.BelowPosition,
                forwardVector = player.Character.ForwardVector,
                velocity = player.Character.Velocity,
                speed = player.Character.Speed,
                isDead = player.Character.IsDead,
                velocityInVehicle = player.Character.CurrentVehicle.Velocity,
                Model = player.Character.Model.Name,
            };
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