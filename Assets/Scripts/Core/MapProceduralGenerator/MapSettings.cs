using UnityEngine;

namespace Core.MapProceduralGenerator
{
    public class MapSettings
    {
        public (int, int) RoadsCount = (1, 4);
        public (int, int) SpawnpointsCount = (4, 6); //SpawnpointsCount >= RoadsCount

        public bool Validate()
        {
            if (SpawnpointsCount.Item1 < RoadsCount.Item1)
            {
                Debug.LogError("MinSpawnpointsCount should be >= MinRoadsCount");
                return false;
            }

            return true;
        }
    }
}