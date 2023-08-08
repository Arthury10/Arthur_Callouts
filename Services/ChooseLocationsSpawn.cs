using Rage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArthurCallouts.Services
{
    internal class ChooseLocationsSpawn
    {
        public Vector3 GetPlayerPosition()
        {
            return Game.LocalPlayer.Character.Position;
        } 
    }
}
