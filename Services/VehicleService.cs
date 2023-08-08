using Rage;
using System;

namespace ArthurCallouts.Services
{
    public class VehicleService
    {
        public Random _randomVehicle = new Random();

        public Vehicle CreateVehicle(string model = null)
        {
            // Se o modelo for fornecido, use esse modelo, caso contrário, gere aleatoriamente.
            if (string.IsNullOrEmpty(model))
            {
                Vehicle randomVehicle = new Vehicle(Vector3.Zero);
                return randomVehicle;
            }
            else
            {
                return new Vehicle(model, Vector3.Zero);
            }
        }
    }
}
