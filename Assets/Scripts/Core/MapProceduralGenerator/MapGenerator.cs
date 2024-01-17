using System;
using System.Collections.Generic;
using System.Linq;
using Tools.Gizmo;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.MapProceduralGenerator
{
    public class MapGenerator : IMapGenerator
    {
        private readonly IGizmoDrawerFactory _gizmoDrawerFactory;
        private int _seed;
        private MapSettings _mapSettings;
        private MapRoad[] _roads;
        private int _spawnpointsCount;
        private int[,] map = new int[100, 100];

        public MapGenerator(IGizmoDrawerFactory gizmoDrawerFactory)
        {
            _gizmoDrawerFactory = gizmoDrawerFactory;
        }
        
        public void CreateMap()
        {
            CreateMap(new MapSettings(), -1);
        }
        
        public void CreateMap(MapSettings mapSettings, int seed = -1)
        {
            if (!mapSettings.Validate())
                return;
            
            _mapSettings = mapSettings;
            SetSeed(seed);
            _spawnpointsCount = Random.Range(_mapSettings.SpawnpointsCount.Item1, _mapSettings.SpawnpointsCount.Item1);
            GenerateRoads(50 ,50);
        }

        private void CreateSpawnpoints()
        {
            
        }

        private void GenerateRoads(int xStartPos, int yStartPos)
        {
            int roadsCount = Random.Range(_mapSettings.RoadsCount.Item1, _mapSettings.RoadsCount.Item2);
            _roads = new MapRoad[roadsCount];
            
            int restSpawnpoints = _spawnpointsCount;
            
            for (int i = 0; i < roadsCount; i++)
            {
                int spawnpointsOnRoadMaxCount = restSpawnpoints - (roadsCount - (i + 1));
                int spawnpointsOnRoadCount = restSpawnpoints;
                if (roadsCount - i > 1)
                {
                    spawnpointsOnRoadCount = Random.Range(1, spawnpointsOnRoadMaxCount);
                }

                restSpawnpoints -= spawnpointsOnRoadCount;
                var road = new MapRoad(_gizmoDrawerFactory, spawnpointsOnRoadCount);
                road.SetWaypoints(new Vector2(xStartPos, yStartPos));
                _roads[i] = road;
            }
            map[xStartPos, yStartPos] = 1;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < roadsCount; j++)
                {
                    Vector2 last = _roads[j].PointsList.Last();

                    List<Vector2> freeCells = new List<Vector2>(4);
                    freeCells.Add(new Vector2(last.x + 1, last.y));
                    freeCells.Add(new Vector2(last.x - 1, last.y));
                    freeCells.Add(new Vector2(last.x, last.y + 1));
                    freeCells.Add(new Vector2(last.x, last.y - 1));

                    for (int k = freeCells.Count-1; k >= 0; k--)
                    {
                        if (map[(int) freeCells[k].x, (int) freeCells[k].y] == 1)
                        {
                            freeCells.RemoveAt(k);
                        }
                    }
                    
                    var chosenDestination = Random.Range(0, freeCells.Count);
                    map[(int)freeCells[chosenDestination].x, (int)freeCells[chosenDestination].y] = 1;
                    _roads[j].SetWaypoints(freeCells[chosenDestination]);
                }
            }
        }
        
        private void SetSeed(int seed = -1)
        {
            if (seed < 0)
            {
                _seed = Random.Range(0, Int32.MaxValue);
            }
            
            Random.InitState(_seed);
        }
    }
}