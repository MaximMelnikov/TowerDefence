using UnityEngine;

namespace Core.Bootstrap.MapProceduralGenerator
{
    public class MapSettings
    {
        public (int, int) distanceFromStart = (0, 2);
        public (int, int) RoadsCount = (5, 10);
        public (int, int) SpawnpointsCount = (30, 60); //SpawnpointsCount >= RoadsCount

        public bool Validate()
        {
            if (SpawnpointsCount.Item1 < RoadsCount.Item1)
            {
                Debug.LogError("MinSpawnpointsCount should be >= MaxRoadsCount");
                return false;
            }

            return true;
        }
    }
}