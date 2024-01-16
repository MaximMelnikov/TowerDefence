using System;
using System.Collections.Generic;
using System.Linq;
using Core.Factory.Gizmo;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Bootstrap.MapProceduralGenerator
{
    public class MapGenerator : IMapGenerator
    {
        private readonly IGizmoDrawerFactory _gizmoDrawerFactory;
        private const int SegmentsCount = 12;
        private int _seed;
        private MapSettings _mapSettings;
        private MapRoad[] _roads;
        private Ellipse[] _ellipses;
        private int _spawnpointsCount;

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

            CreateEllipses();
            _spawnpointsCount = Random.Range(_mapSettings.SpawnpointsCount.Item1, _mapSettings.SpawnpointsCount.Item2);
            GenerateRoads(0, 0);
        }

        private void CreateEllipses()
        {
            _ellipses = new Ellipse[_mapSettings.distanceFromStart.Item2 - _mapSettings.distanceFromStart.Item1];
            int index = 0;
            for (int i = _mapSettings.distanceFromStart.Item1; i < _mapSettings.distanceFromStart.Item2; i++)
            {
                _ellipses[index] = new Ellipse(_gizmoDrawerFactory, 0, 0, i+1, i+1);
                index++;
            }
        }

        private void GenerateRoads(float xStartPos, float yStartPos)
        {
            int roadsCount = Random.Range(_mapSettings.RoadsCount.Item1, _mapSettings.RoadsCount.Item1);
            _roads = new MapRoad[roadsCount];

            int restSpawnpoints = _spawnpointsCount;
            
            List<int> availableSegments = Enumerable.Range(0, SegmentsCount).ToList();
            
            for (int i = 0; i < roadsCount; i++)
            {
                int randIndex = Random.Range(0, availableSegments.Count);
                int randEllipseDestinationIndex = Random.Range(0, _ellipses.Length);
                
                int spawnpointsOnRoadMaxCount = restSpawnpoints - (roadsCount - (i + 1));
                int spawnpointsOnRoadCount = restSpawnpoints;
                if (roadsCount - i > 1)
                {
                    spawnpointsOnRoadCount = Random.Range(1, spawnpointsOnRoadMaxCount);
                }

                restSpawnpoints -= spawnpointsOnRoadCount;
                
                _roads[i] = new MapRoad(_gizmoDrawerFactory, availableSegments[randIndex], _ellipses[randEllipseDestinationIndex], spawnpointsOnRoadCount);
                availableSegments.RemoveAt(randIndex);
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