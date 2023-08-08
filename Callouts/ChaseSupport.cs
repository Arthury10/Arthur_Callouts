using Rage;
using LSPD_First_Response.Mod.Callouts;
using LSPD_First_Response.Mod.API;
using System.Drawing;
using CalloutInterfaceAPI;

namespace ArthurCallouts.Callouts
{
[CalloutInterface("Pessoa suspeita reportada", CalloutProbability.High, "Pessoa suspeita", "Code 1", "LSPD")]
    public class ChaseSupport : Callout
    {

        private Ped _Suspect1;
        private Vehicle _SuspectVehicle;
        private Vector3 _SpawnPoint;
        private Blip _Blip;
        private LHandle _Pursuit;

        public override bool OnBeforeCalloutDisplayed()
        {
            _SpawnPoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));
            ShowCalloutAreaBlipBeforeAccepting(_SpawnPoint, 50f);
            CalloutMessage = "Apoio em Perseguição";
            CalloutPosition = _SpawnPoint;
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_SUSPECT_ON_THE_RUN IN_OR_ON_POSITION", _SpawnPoint);
            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            _SuspectVehicle = new Vehicle(_SpawnPoint);
            _SuspectVehicle.IsPersistent = true;

            _Suspect1 = _SuspectVehicle.CreateRandomDriver();
            _Suspect1.BlockPermanentEvents = true;
            _Suspect1.Tasks.CruiseWithVehicle(_SuspectVehicle, 100f, VehicleDrivingFlags.FollowTraffic);

            _Blip = _Suspect1.AttachBlip();
            _Blip.Color = Color.Red;

            _Pursuit = LSPD_First_Response.Mod.API.Functions.CreatePursuit();
            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(_Pursuit, _Suspect1);

            // Adicionando outros policiais à perseguição
            for (int i = 0; i < 2; i++)
            {
                Ped police = new Ped(_SpawnPoint.Around(10f));
                police.BlockPermanentEvents = true;
                police.Tasks.FightAgainstClosestHatedTarget(100f);
                LSPD_First_Response.Mod.API.Functions.AddCopToPursuit(_Pursuit, police);
            }

            return base.OnCalloutAccepted();
        }

        public override void Process()
        {
            if (Game.LocalPlayer.Character.IsDead) End();
            if (_Suspect1 && _Suspect1.IsDead) End();
            if (_Suspect1 && LSPD_First_Response.Mod.API.Functions.IsPedArrested(_Suspect1)) End();

            base.Process();
        }

        public override void End()
        {
            if (_Suspect1) _Suspect1.Dismiss();
            if (_SuspectVehicle) _SuspectVehicle.Dismiss();
            if (_Blip) _Blip.Delete();
            Game.DisplayNotification("Despacho: Código 4. Todas as unidades, voltem a patrulhar.");
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("ATTENTION_THIS_IS_DISPATCH_HIGH ALL_UNITS_CODE4 NO_FURTHER_UNITS_REQUIRED");
            base.End();
        }
    }
}

