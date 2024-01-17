using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools.Gizmo;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.MapProceduralGenerator
{
    public class MapGenerator : IMapGenerator
    {
        private const int RoadLength = 10;
        private readonly IGizmoDrawerFactory _gizmoDrawerFactory;
        private readonly MapTilesDatabase _mapTilesDatabase;
        private int _seed;
        private MapSettings _mapSettings;
        private MapRoad[] _roads;
        private int _spawnpointsCount;
        private int[,] map = new int[100, 100];
        private RectInt borders;

        public MapGenerator(IGizmoDrawerFactory gizmoDrawerFactory, MapTilesDatabase mapTilesDatabase)
        {
            _gizmoDrawerFactory = gizmoDrawerFactory;
            _mapTilesDatabase = mapTilesDatabase;
        }
        
        public void CreateMap()
        {
            CreateMap(new MapSettings(), -1);
        }
        
        public async Task CreateMap(MapSettings mapSettings, int seed = -1)
        {
            List<Task> tasks = new List<Task>();
            if (!mapSettings.Validate())
                return;
            
            _mapSettings = mapSettings;
            SetSeed(seed);
            _spawnpointsCount = Random.Range(_mapSettings.SpawnpointsCount.Item1, _mapSettings.SpawnpointsCount.Item1);
            
            GenerateRoads(50 ,50);
            tasks.Add(DrawRoads());
            
            borders = GetMapBorders();
            tasks.Add(DrawGrass());
            
            tasks.Add(CreateCastle());
            
            await Task.WhenAll(tasks);
        }

        private async Task DrawGrass()
        {
            List<Task> tasks = new List<Task>();
            for (int x = borders.xMin; x < borders.xMax+1; x++)
            {
                for (int y = borders.yMin; y < borders.yMax+1; y++)
                {
                    if (map[x,y] == 0)
                    {
                        bool placeSomeProp = Random.Range(0, 100) > 70;
                        int propIndex = Random.Range(0, _mapTilesDatabase.Decorations.Count);

                        if (placeSomeProp)
                        {
                            DrawProp(propIndex, new Vector2(x, y), 0);
                        }
                        else
                        {
                            DrawTile(TileType.Grass, new Vector2(x, y), 0);
                        }
                    }
                }
            }
            
            await Task.WhenAll(tasks);
        }
        
        private async Task DrawTile(TileType type, Vector2 position, int rotation)
        {
            await 
                _mapTilesDatabase.GetTile(type)
                    .InstantiateAsync(position.ToVector3WithYToZ(), Quaternion.Euler(0, rotation, 0)).Task;
        }
        
        private async Task DrawProp(int index, Vector2 position, int rotation)
        {
            await 
                _mapTilesDatabase.Decorations[index]
                    .InstantiateAsync(position.ToVector3WithYToZ(), Quaternion.Euler(0, rotation, 0)).Task;
        }

        private void GenerateRoads(int xStartPos, int yStartPos)
        {
            //random roads count
            int roadsCount = Random.Range(_mapSettings.RoadsCount.Item1, _mapSettings.RoadsCount.Item2);
            _roads = new MapRoad[roadsCount];
            
            //randomize spawnpoints count on roads and create roads startpoints
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
                var road = new MapRoad(_gizmoDrawerFactory, spawnpointsOnRoadCount, _mapTilesDatabase);
                road.SetWaypoints(new Vector2(xStartPos, yStartPos));
                _roads[i] = road;
            }
            map[xStartPos, yStartPos] = 1;
            
            //generate roads waypoints
            for (int i = 0; i < RoadLength; i++)
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
                    if (freeCells.Count == 0)
                    {
                        continue;
                    }
                    map[(int)freeCells[chosenDestination].x, (int)freeCells[chosenDestination].y] = 1;
                    _roads[j].SetWaypoints(freeCells[chosenDestination]);
                }
            }
        }

        private async Task DrawRoads()
        {
            List<Task> tasks = new List<Task>(_roads.Length);
            foreach (var road in _roads)
            {
                tasks.Add(road.DrawRoad());
            }
            
            await Task.WhenAll(tasks);
        }
        
        private async Task CreateCastle()
        {
            await DrawTile(TileType.Castle, new Vector2(50, 50), 0);
        }
        
        private RectInt GetMapBorders()
        {
            var minX = (int)_roads.Min(road => road.Points.Min(point => point.x));
            var maxX = (int)_roads.Max(road => road.Points.Max(point => point.x));
            var minY = (int)_roads.Min(road => road.Points.Min(point => point.y));
            var maxY = (int)_roads.Max(road => road.Points.Max(point => point.y));
            return new RectInt(minX, minY, maxX - minX, maxY - minY);
        }
        
        private void SetSeed(int seed = -1)
        {
            if (seed < 0)
            {
                _seed = Random.Range(0, Int32.MaxValue);
            }
            Debug.Log($"Map seed: {_seed}");
            Random.InitState(_seed);
        }
    }
}