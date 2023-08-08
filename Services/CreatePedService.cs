using Rage;
using System;

namespace ArthurCallouts.Services
{
    public class CreatePedService
    {
        private Ped _Ped;

        //vamos criar um ped, mas temos que passar o modelo dele, a posição e a heading (rotação) dele, mas caso não seja passado o modelo ele deve ser um modelo aletorio
        public Ped CreatePed(string model, Vector3 position, float heading)
        {
            if (model == null)
            {
                _Ped = new Ped(position, heading);
            }
            else
            {
                _Ped = new Ped(model, position, heading);
            }

            return _Ped;
        }
    }

    public class CreateGroupOfPedsService
    {
        private Ped[] _PedGroup;

        // Método para criar um grupo de peds
        public Ped[] CreateGroupOfPeds(string model, Vector3 position, float heading, int numberOfPeds)
        {
            _PedGroup = new Ped[numberOfPeds];

            for (int i = 0; i < numberOfPeds; i++)
            {
                Vector3 pos = position; // Você pode querer ajustar a posição para cada Ped
                pos.X += i * 2; // Ajusta a posição X para cada Ped

                if (model == null)
                {
                    _PedGroup[i] = new CreatePedService().CreatePed(null, pos, heading);
                }
                else
                {
                    _PedGroup[i] = new CreatePedService().CreatePed(model, pos, heading);
                }
                
            }

            return _PedGroup;
        }
    }
}
