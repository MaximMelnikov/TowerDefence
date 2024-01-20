using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.MapProceduralGenerator.MapFactory;
using Cysharp.Threading.Tasks;
using Tools.Gizmo;
using UnityEngine;
using Camera = Core.Gameplay.Camera;
using Random = UnityEngine.Random;

namespace Core.Services.MapProceduralGenerator
{
    public class MapGenerator : IMapGenerator
    {
        private readonly IMapFactory _mapFactory;
        private readonly IGizmoDrawerFactory _gizmoDrawerFactory;
        private readonly MapTilesDatabase _mapTilesDatabase;
        private MapSettings _mapSettings;
        
        private int _seed;
        private Rect _borders;
        private MapRoad[] _roads;
        private int[,] _map;

        public MapGenerator(IGizmoDrawerFactory gizmoDrawerFactory, MapTilesDatabase mapTilesDatabase, IMapFactory mapFactory)
        {
            _mapFactory = mapFactory;
            _gizmoDrawerFactory = gizmoDrawerFactory;
            _mapTilesDatabase = mapTilesDatabase;
        }
        
        public async UniTask CreateMap(MapSettings mapSettings, int seed = -1)
        {
            Reset();
            if (!mapSettings.Validate())
                return;
            
            List<UniTask> tasks = new List<UniTask>();
            
            _mapSettings = mapSettings;
            _map = new int[_mapSettings.MapSize.x, _mapSettings.MapSize.y];
            
            SetSeed(seed);
            
            GenerateRoads(_mapSettings.StartPos.x ,_mapSettings.StartPos.y);
            tasks.Add(DrawRoads());
            tasks.Add(GenerateTowers());
            _borders = GetMapBorders();
            tasks.Add(FillMapByGrassAndProps());
            tasks.Add(_mapFactory.CreateObject(TileType.Castle, _mapSettings.StartPos, 0));

            UnityEngine.Camera.main.GetComponent<Camera>().rect = _borders;//temp camera
            
            await UniTask.WhenAll(tasks);
        }
        
        private void SetSeed(int seed = -1)
        {
            _seed = seed;
            if (seed < 0)
            {
                _seed = Random.Range(0, Int32.MaxValue);
            }
            Debug.Log($"Map seed: {_seed}");
            Random.InitState(_seed);
        }
        
        private Rect GetMapBorders()
        {
            var minX = _roads.Min(road => road.LineGizmoPoints.Min(point => point.x));
            var maxX = _roads.Max(road => road.LineGizmoPoints.Max(point => point.x));
            var minY = _roads.Min(road => road.LineGizmoPoints.Min(point => point.y));
            var maxY = _roads.Max(road => road.LineGizmoPoints.Max(point => point.y));
            return new Rect(minX-1, minY-1, maxX - minX + 2, maxY - minY + 2);
        }

        private async UniTask FillMapByGrassAndProps()
        {
            List<UniTask> tasks = new List<UniTask>();
            for (int x = (int)_borders.xMin; x < (int)_borders.xMax+1; x++)
            {
                for (int y = (int)_borders.yMin; y < (int)_borders.yMax+1; y++)
                {
                    if (_map[x,y] > 0)
                        continue;
                    
                    _map[x, y] = 1;
                    bool placeSomeProp = Random.Range(0, 100) > 100-_mapSettings.ChanceToSpawnPropPercent;
                    int propIndex = Random.Range(0, _mapTilesDatabase.Decorations.Count);

                    if (placeSomeProp)
                    {
                        tasks.Add(_mapFactory.CreateProp(propIndex, new Vector2(x, y), 0));
                    }
                    else
                    {
                        tasks.Add(_mapFactory.CreateObject(TileType.Grass, new Vector2(x, y), 0));
                    }
                }
            }
            await UniTask.WhenAll(tasks);
        }
        
        private async UniTask GenerateTowers()
        {
            List<UniTask> tasks = new List<UniTask>();
            foreach (var road in _roads)
            {
                var emptyCells = new List<Vector2>();
                for (int i = road.LineGizmoPoints.Length - 2; i > 1; i--)
                {
                    List<Vector2> neighbours = new List<Vector2>(4)
                    {
                        new(road.LineGizmoPoints[i].x + 1, road.LineGizmoPoints[i].y),
                        new(road.LineGizmoPoints[i].x - 1, road.LineGizmoPoints[i].y),
                        new(road.LineGizmoPoints[i].x, road.LineGizmoPoints[i].y + 1),
                        new(road.LineGizmoPoints[i].x, road.LineGizmoPoints[i].y - 1)
                    };

                    foreach (var neighbour in neighbours)
                    {
                        if (_map[(int)neighbour.x, (int)neighbour.y] == 0)
                        {
                            emptyCells.Add(neighbour);
                        }
                    }
                }

                if (emptyCells.Count == 0)
                {
                    continue;
                }
                
                int maxTowersCount = Mathf.Clamp(_mapSettings.MaxTowersOnRoad, 0, emptyCells.Count);
                for (int i = 0; i < maxTowersCount; i++)
                {
                    var randomCell = Random.Range(0, emptyCells.Count);
                    Vector3 offset = new Vector3(0, 0.1f, 0);
                    tasks.Add(_mapFactory.CreateObject(TileType.TowerSlot, emptyCells[randomCell], 0, .1f));
                    tasks.Add(_mapFactory.CreateObject(TileType.Grass, emptyCells[randomCell], 0));
                    _map[(int)emptyCells[randomCell].x, (int)emptyCells[randomCell].y] = 1;
                    emptyCells.RemoveAt(randomCell);
                }
            }
            await UniTask.WhenAll(tasks);
        }

        private void GenerateRoads(int xStartPos, int yStartPos)
        {
            //randomize roads count
            int roadsCount = Random.Range(_mapSettings.RoadsCount.Item1, _mapSettings.RoadsCount.Item2);
            _roads = new MapRoad[roadsCount];
            _map[xStartPos, yStartPos] = 1;

            for (int i = 0; i < roadsCount; i++)
            {
                _roads[i] = new MapRoad(_gizmoDrawerFactory, _mapFactory);
                _roads[i].AddWaypoint(new Vector2(xStartPos, yStartPos));
            }
            
            //generate roads waypoints
            for (int i = 1; i < _mapSettings.RoadLength; i++)
            {
                for (int j = 0; j < roadsCount; j++)
                {
                    Vector2 last = _roads[j].Waypoints.Last();

                    List<Vector2> freeCells = new List<Vector2>(4)
                    {
                        new(last.x + 1, last.y),
                        new(last.x - 1, last.y),
                        new(last.x, last.y + 1),
                        new(last.x, last.y - 1)
                    };

                    for (int k = freeCells.Count-1; k >= 0; k--)
                    {
                        if (_map[(int) freeCells[k].x, (int) freeCells[k].y] != 0)
                        {
                            freeCells.RemoveAt(k);
                        }
                    }
                    
                    var chosenDestination = Random.Range(0, freeCells.Count);
                    if (freeCells.Count == 0)
                    {
                        continue;
                    }
                    _map[(int)freeCells[chosenDestination].x, (int)freeCells[chosenDestination].y] = 2;
                    _roads[j].AddWaypoint(freeCells[chosenDestination]);
                }
            }
        }

        private async UniTask DrawRoads()
        {
            List<UniTask> tasks = new List<UniTask>(_roads.Length);
            foreach (var road in _roads)
            {
                tasks.Add(road.DrawRoad());
            }
            
            await UniTask.WhenAll(tasks);
        }

        public void Reset()
        {
            _map = null;
            _mapFactory.Reset();

            if (_roads != null)
            {
                foreach (var road in _roads)
                {
                    road.Dispose();
                }
                _roads = null;
            }
        }
    }
}