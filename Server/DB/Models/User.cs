using Rage;
using System;

namespace ArthurCallouts.Server.DB.Models
{
    [Serializable] // Necessário para a serialização binária
    public class UserModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int Armor { get; set; }
        public int Money { get; set; }
        public bool IsMale { get; set; }
        public  Vector3 Position { get; set; }
        public Vector3 AbovePosition { get; set; }
        public Vector3 BelowPosition { get; set; }
        public Vector3 ForwardVector { get; set; }
        public Vector3 Velocity { get; set; }
        public float Speed { get; set; }
        public bool IsDead { get; set; }
        public string Model { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int ExperienceToNextLevel { get; set; }
        public string Office { get; set; }
        // Outras propriedades relevantes aqui
    }
}
