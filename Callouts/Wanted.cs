using System;
using Rage;
using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Engine.Scripting.Entities;

namespace ArthurCallouts.Callouts
{
    [CalloutInterface("Procurado", CalloutProbability.High, "Pessoa suspeito", "Code 1", "LSPD")]
    public class Wanted : Callout
    {
        private Ped _Suspect;
        private Vehicle _Vehicle;
        private Vector3 _SpawnPoint;
        private Blip _Blip;
        private Random _Random = new Random();
        private LHandle _Pursuit;
        private bool _PursuitCreated = false;

        public override bool OnBeforeCalloutDisplayed()
        {
            _SpawnPoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));

            ShowCalloutAreaBlipBeforeAccepting(_SpawnPoint, 50f);

            CalloutMessage = "Pessoa ou veículo suspeito";
            CalloutPosition = _SpawnPoint;
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("SUSPICIOUS_PERSON_OR_VEHICLE", _SpawnPoint);

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            // Randomly decide if the suspect is in a vehicle.
            if (_Random.NextDouble() < 0.5)
            {
                _Vehicle = new Vehicle(_SpawnPoint);
                _Vehicle.IsPersistent = true;

                _Suspect = _Vehicle.CreateRandomDriver();
            }
            else
            {
                _Suspect = new Ped(_SpawnPoint);
            }

            _Suspect.BlockPermanentEvents = true;

            _Blip = _Suspect.AttachBlip();
            _Blip.IsFriendly = false;

            return base.OnCalloutAccepted();
        }

        public override void Process()
        {
            if (_Suspect && _Suspect.Position.DistanceTo(Game.LocalPlayer.Character.Position) < 2f)
            {
                bool suspectHasWeapon = _Suspect.Inventory.Weapons.Count > 0;

                if (suspectHasWeapon)
                {
                    if (_Vehicle && _Suspect.IsInVehicle(_Vehicle, false))
                    {
                        _Suspect.Tasks.CruiseWithVehicle(_Vehicle, 100f, VehicleDrivingFlags.FollowTraffic);
                    }
                    else
                    {
                        _Suspect.Tasks.Flee(Game.LocalPlayer.Character, 1000f, -1);
                    }

                    if (!_PursuitCreated)
                    {
                        _Pursuit = LSPD_First_Response.Mod.API.Functions.CreatePursuit();
                        LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(_Pursuit, _Suspect);
                        LSPD_First_Response.Mod.API.Functions.SetPursuitIsActiveForPlayer(_Pursuit, true);
                        LSPD_First_Response.Mod.API.Functions.RequestBackup(_SpawnPoint, LSPD_First_Response.EBackupResponseType.Pursuit, LSPD_First_Response.EBackupUnitType.LocalUnit);
                        _PursuitCreated = true;
                    }
                }
            }

            if (Game.IsKeyDown(Settings.EndCall)) End();
            if (Game.LocalPlayer.Character.IsDead) End();
            if (_Suspect && _Suspect.IsDead) End();
            if (_Suspect && LSPD_First_Response.Mod.API.Functions.IsPedArrested(_Suspect)) End();
            base.Process();
        }

        public override void End()
        {
            if (_Suspect) _Suspect.Dismiss();
            if (_Vehicle) _Vehicle.Dismiss();
            if (_Blip) _Blip.Delete();
            Game.DisplayNotification("Despacho: Código 4. Todas as unidades, voltem a patrulhar.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();
        }
    }
}
