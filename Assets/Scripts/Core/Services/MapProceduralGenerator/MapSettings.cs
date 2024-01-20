using UnityEngine;

namespace Core.Services.MapProceduralGenerator
{
    public class MapSettings
    {
        public Vector2Int MapSize { get; } = new(50, 50);
        public Vector2Int StartPos { get; } = new(25, 25);
        public int RoadLength { get; } = 10;
        public int MaxTowersOnRoad { get; } = 3;
        public (int, int) RoadsCount { get; } = (1, 4);
        public int ChanceToSpawnPropPercent { get; } = 30;

        public MapSettings()
        {
        }
        
        public MapSettings(
            int roadLength,
            int maxTowersOnRoad,
            (int, int) roadsCount,
            int chanceToSpawnPropPercent)
        {
            RoadLength = roadLength;
            MaxTowersOnRoad = maxTowersOnRoad;
            RoadsCount = roadsCount;
            ChanceToSpawnPropPercent = chanceToSpawnPropPercent;
        }
        
        public bool Validate()
        {
            return true;
        }
    }
}