using Rage;
using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;

using ArthurCallouts.Services;
using System;
using System.Collections.Generic;

namespace ArthurCallouts.Callouts
{
    [CalloutInterface("Pessoa suspeita reportada (YES)", CalloutProbability.High, "Pessoa suspeita (Oque é?)", "Code 1", "LSPD")]
    public class SuspiciousPerson : Callout
    {
        private CreateGroupOfPedsService _GroupOfPedsService;
        private ChooseLocationsSpawn _ChooseLocationsSpawn;

        private bool _isDriver;

        private Ped[] _Suspects;

        private List<Ped> _processedSuspects = new List<Ped>();

        private Group _GroupOfPeds;
        private RelationshipGroup _SuspectRelationship;

        private string _SuspectSpawnZone;

        private int _NumberOfSuspects = 1;

        private Vehicle _Vehicle;

        private Vector3 _SpawnPoint;

        private Blip[] _Blips;

        private readonly Random _Random = new Random();


        private bool _PatDown = false;

        //private LHandle _Pursuit;
        //private bool _PursuitCreated = false;

        private float _VehicleSpeed = 0f;

        public override bool OnBeforeCalloutDisplayed()
        {

            _ChooseLocationsSpawn = new ChooseLocationsSpawn();

            if (_Random.NextDouble() < 0.5)
            {
                _isDriver = true;
                _SpawnPoint = _ChooseLocationsSpawn.SpawnOnStreet(1000f);
            } else
            {
                _isDriver = false;
                _SpawnPoint = _ChooseLocationsSpawn.SpawnOnSideWalk(1000f);
            }

            _SuspectSpawnZone = World.GetStreetName(World.GetStreetHash(_SpawnPoint));

            ShowCalloutAreaBlipBeforeAccepting(_SpawnPoint, 50f);

            CalloutMessage = "Individuo Suspeito" + " no " + _SuspectSpawnZone + " " + " reportado";

            CalloutPosition = _SpawnPoint;
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_GRAND_THEFT_AUTO IN_OR_ON_POSITION", _SpawnPoint);
            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {

            _NumberOfSuspects = _Random.Next(1, 5);
            
            _GroupOfPedsService = new CreateGroupOfPedsService();
            
            _Suspects = _GroupOfPedsService.CreateGroupOfPeds(null, _SpawnPoint, -1, _NumberOfSuspects);

            _Blips = new Blip[_Suspects.Length];

            _GroupOfPeds = new Group(_Suspects[0]);
            _SuspectRelationship = new RelationshipGroup("SUSPECTS");

            for (int i = 0; i < _Suspects.Length; i++)
             {
                _Suspects[i].Tasks.Wander();
                _Suspects[i].RelationshipGroup.SetRelationshipWith(_SuspectRelationship, Relationship.Companion);
                _Suspects[i].StaysInGroups = true;
                //_Suspects[i].Group.AddMember(_Suspects[i]);

                _Blips[i] = _Suspects[i].AttachBlip();
                _Blips[i].Color = System.Drawing.Color.Yellow;
                _Blips[0].EnableRoute(System.Drawing.Color.Yellow);
            
                if (_Random.NextDouble() < 0.5)
                {
                    _Suspects[i].Inventory.GiveNewWeapon("WEAPON_PISTOL", 100, false);
                    StopThePed.API.Functions.injectPedSearchItems(_Suspects[i]);
                }
             }

            //Condicional para verificar se o suspeito está em um veículo ou não.
            if (_isDriver)
            {
                _VehicleSpeed = _Random.Next(30, 141);
           
                _Vehicle = new Vehicle("intruder", _SpawnPoint);

                for(int i = 0; i < _Suspects.Length; i++)
                {
                    _Suspects[i].WarpIntoVehicle(_Vehicle, i -1);
                    _Suspects[0].Tasks.CruiseWithVehicle(_VehicleSpeed, VehicleDrivingFlags.FollowTraffic);
                }

                if(_Suspects.Length > 1)
                {
                    Game.DisplayNotification("Os suspeitos estão em um veículo, possívelmente armados.");
                } else
                {
                    Game.DisplayNotification("O suspeito está em um veículo, possívelmente armado.");
                }


                return base.OnCalloutAccepted();
            }

            if (_Suspects.Length > 1)
            {
                Game.DisplayNotification("Os suspeitos estão a pé, possívelmente armados.");
            }
            else
            {
                Game.DisplayNotification("O suspeito está a pé, possívelmente armado.");
            }

            return base.OnCalloutAccepted();
        }

        public override void Process()
        {   

            for(int i = 0; i < _Suspects.Length; i++)
            {
                if (_Suspects[i] && _Suspects[i].Inventory.Weapons.Count > 0)
                {
                    StopThePed.API.STPPedEventHandler Events_PatDownPedEvent = (ped) =>
                    {
                        _PatDown = true;
                    };

                    StopThePed.API.Events.patDownPedEvent += Events_PatDownPedEvent;

                    if (_PatDown)
                    {
                        _Suspects[i].Tasks.FightAgainst(Game.LocalPlayer.Character);
                        _Blips[i].Color = System.Drawing.Color.Red;
                    }

                }

            }

            if (Game.IsKeyDown(Settings.EndCall)) End();
            if (Game.LocalPlayer.Character.IsDead) End();


            for (int i = 0; i < _Suspects.Length; i++)
            {
                Ped suspect = _Suspects[i];
                if (suspect != null && !_processedSuspects.Contains(suspect) &&
                    (suspect.IsDead || LSPD_First_Response.Mod.API.Functions.IsPedArrested(suspect)))
                {
                    _processedSuspects.Add(suspect); // Adicione os suspeitos processados à lista
                    _Blips[i].Delete(); // Remova o blip do suspeito
                }
            }

            if (_processedSuspects.Count == _Suspects.Length)
            {
                End();
            }

            base.Process();
        }

        public override void End()
        {
            for(int i = 0; i < _Suspects.Length; i++)
            {
                if (_Suspects[i]) _Suspects[i].Dismiss();
                if (_Blips[i]) _Blips[i].Delete();
            }

            if (_Vehicle) _Vehicle.Dismiss();
            Game.DisplayNotification("Despacho: Código 4. Todas as unidades, voltem a patrulhar.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            _PatDown = false;
            _Vehicle = null;
            _Suspects = null;
            _Blips = null;
            base.End();
        }
    }
}