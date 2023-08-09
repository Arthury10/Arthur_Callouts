using Rage;


namespace ArthurCallouts.Services
{
    internal class ChooseLocationsSpawn
    {
        public Vector3 SpawnOnSideWalk(float radius)
        {

            Vector3 playerPosition = Game.LocalPlayer.Character.Position.Around(1000f);

            return World.GetNextPositionOnStreet(playerPosition.Around(radius));
        }

        public Vector3 SpawnOnStreet(float radius)
        {
            Vector3 playerPosition = Game.LocalPlayer.Character.Position.Around(1000f);

            return World.GetNextPositionOnStreet(playerPosition.Around(radius));
        }
    }
}
