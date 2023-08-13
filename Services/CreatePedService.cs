using Rage;
using System;

namespace ArthurCallouts.Services
{
    public class CreatePedService
    {
        private LoggerService _Logger = new LoggerService();
        private Ped[] _PedGroup;


        //vamos criar um ped, mas temos que passar o modelo dele, a posição e a heading (rotação) dele, mas caso não seja passado o modelo ele deve ser um modelo aletorio
        public Ped CreatePed(string model, Vector3 position, float heading)
        {
            Ped ped = model == null
                ? new Ped(position, heading)
                : new Ped(model, position, heading);

            _Logger.Info($"Ped criado{(model != null ? " " + model : "")}");

            return ped;
        }

        /// <summary>
        /// Cria um grupo de peds com um modelo específico, posição e direção.
        /// </summary>
        /// <param name="model">O modelo dos peds. Se null, um modelo aleatório será usado.</param>
        /// <param name="position">A posição inicial dos peds.</param>
        /// <param name="heading">A direção inicial dos peds.</param>
        /// <param name="numberOfPeds">O número de peds a ser criado.</param>
        /// <returns>Uma matriz de peds criados.</returns>
        public Ped[] CreateGroupOfPeds(string model, Vector3 position, float heading, int numberOfPeds)
        {
            _PedGroup = new Ped[numberOfPeds];
            _Logger.Info($"Ped a ser criado {numberOfPeds}");

            for (int i = 0; i < numberOfPeds; i++)
            {
                Vector3 pos = position + new Vector3(i * 2, 0, 0); // Ajusta a posição X para cada Ped
                _PedGroup[i] = CreatePed(model, pos, heading);
                _Logger.Info($"Ped criado {(model != null ? model + " " : "")}{i}");
            }

            return _PedGroup;
        }
    }
}
