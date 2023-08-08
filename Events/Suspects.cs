using ArthurCallouts.Services;
using LSPD_First_Response.Mod.API;
using Rage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArthurCallouts.Events
{
    public struct Passenger
    {
        public Ped Pedestrian;
        public WeaponStatus WeaponStatus;
        public IdentityStatus IdentityStatus;
    }

    internal class Suspects
    {
        private RandomizationService _RandomizationService = new RandomizationService();

        private Ped _Suspect;
        private Vehicle _Vehicle;
        private List<Passenger> _Passengers;
        private Blip _Blip;
        private LHandle _Pursuit;
        private bool _PursuitCreated = false;
        private bool _PursuitDeleted = false;
        private bool _PursuitUpdated = false;
        private DateTime _LastUpdate = DateTime.Now;
        private static List<Suspects> _AllSuspects = new List<Suspects>();
        private Random _Random = new Random();

        public Suspects(Ped suspect)
        {
            _Suspect = suspect;
            _Passengers = new List<Passenger>();
            _AllSuspects.Add(this);
        }

        public static void Update()
        {
            foreach (Suspects s in _AllSuspects)
            {
                // Update each suspect.
                s.UpdateSuspect();
            }
        }


        public void UpdateSuspect()
        {
            // Check if it's been 5 minutes since the last update.
            if ((DateTime.Now - _LastUpdate).TotalMinutes > 5)
            {
                // If the suspect is within 50m of the player...
                if (Game.LocalPlayer.Character.Position.DistanceTo(_Suspect.Position) < 50)
                {
                    // Count the number of suspects within 50m of the player.
                    int nearbySuspects = _AllSuspects.Count(s => Game.LocalPlayer.Character.Position.DistanceTo(s._Suspect.Position) < 50);

                    // If there are already 2 suspects, do nothing.
                    if (nearbySuspects >= 2)
                        return;

                    // ...mark them as a suspect.
                    _Suspect.Tasks.Wander();
                    _Blip = new Blip(_Suspect.Position, 50f);
                    _Blip.Color = System.Drawing.Color.Yellow;
                    _Blip.Alpha = 0.5f;

                    // Randomize the suspect's attributes.
                    WeaponStatus weaponStatus = _RandomizationService.RandomizeWeaponStatus();
                    DrivingStatus drivingStatus = _RandomizationService.RandomizeDrivingStatus();
                    IdentityStatus identityStatus = _RandomizationService.RandomizeIdentityStatus();
                    TransportMode transportMode = _RandomizationService.RandomizeTransportMode();

                    // Add the suspect's attributes to your game logic here.

                }
                // Record the time of this update.
                _LastUpdate = DateTime.Now;
            }
        }
    }
}
