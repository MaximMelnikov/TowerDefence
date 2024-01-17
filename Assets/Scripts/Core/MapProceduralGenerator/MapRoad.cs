using System;
using System.Collections.Generic;
using Tools.Gizmo;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Task = System.Threading.Tasks.Task;

namespace Core.MapProceduralGenerator
{
    public class MapRoad : IDisposable, ILineGizmoDrawable
    {
        private readonly IGizmoDrawerFactory _gizmoDrawerFactory;
        private readonly int _spawnpointsOnRoadCount;
        private readonly MapTilesDatabase _mapTilesDatabase;

        public bool IsLoop { get => false; }
        public Vector2[] Points { get; private set; }
        public List<Vector2> PointsList = new List<Vector2>();
        
        public MapRoad(
            IGizmoDrawerFactory gizmoDrawerFactory, 
            int spawnpointsOnRoadCount,
            MapTilesDatabase mapTilesDatabase)
        {
            _gizmoDrawerFactory = gizmoDrawerFactory;
            _spawnpointsOnRoadCount = spawnpointsOnRoadCount;
            _mapTilesDatabase = mapTilesDatabase;

            _gizmoDrawerFactory.CreateDrawer(this);
        }

        public void SetWaypoints(Vector2 position)
        {
            PointsList.Add(position);
            Points = PointsList.ToArray();
        }

        public async Task DrawRoad()
        {
            List<Task> tasks = new List<Task>(PointsList.Count);
            tasks.Add(DrawRoadTile(TileType.Crossroad, PointsList[0], 0));
            for (int i = 1; i < PointsList.Count; i++)
            {
                var currentPoint = PointsList[i];
                
                var previousPoint = PointsList[i - 1];
                var previousPointDirection = currentPoint - previousPoint;

                Vector2 nextPoint;

                if (i == PointsList.Count - 1)
                {
                    nextPoint = currentPoint + previousPointDirection;
                }
                else
                {
                    nextPoint = PointsList[i + 1];
                }
                var nextPointDirection = currentPoint - nextPoint;
                
                TileType type = TileType.Straight;
                int rotation = 0;

                if (nextPointDirection == Vector2.up && previousPointDirection == Vector2.down)
                {
                    type = TileType.Straight;
                    rotation = 0;
                }
                else if (nextPointDirection == Vector2.down && previousPointDirection == Vector2.up)
                {
                    type = TileType.Straight;
                    rotation = 180;
                }
                else if (nextPointDirection == Vector2.left && previousPointDirection == Vector2.right)
                {
                    type = TileType.Straight;
                    rotation = 270;
                }
                else if (nextPointDirection == Vector2.right && previousPointDirection == Vector2.left)
                {
                    type = TileType.Straight;
                    rotation = 90;
                }
                else if (nextPointDirection == Vector2.up && previousPointDirection == Vector2.right)
                {
                    type = TileType.Turn;
                    rotation = 270;
                }
                else if (nextPointDirection == Vector2.up && previousPointDirection == Vector2.left)
                {
                    type = TileType.Turn;
                    rotation = 180;
                }
                else if (nextPointDirection == Vector2.down && previousPointDirection == Vector2.right)
                {
                    type = TileType.Turn;
                    rotation = 0;
                }
                else if (nextPointDirection == Vector2.down && previousPointDirection == Vector2.left)
                {
                    type = TileType.Turn;
                    rotation = 90;
                }
                else if (nextPointDirection == Vector2.right && previousPointDirection == Vector2.up)
                {
                    type = TileType.Turn;
                    rotation = 270;
                }
                else if (nextPointDirection == Vector2.right && previousPointDirection == Vector2.down)
                {
                    type = TileType.Turn;
                    rotation = 0;
                }
                else if (nextPointDirection == Vector2.left && previousPointDirection == Vector2.up)
                {
                    type = TileType.Turn;
                    rotation = 180;
                }
                else if (nextPointDirection == Vector2.left && previousPointDirection == Vector2.down)
                {
                    type = TileType.Turn;
                    rotation = 90;
                }
                else if (nextPointDirection == Vector2.up && previousPointDirection == Vector2.down)
                {
                    type = TileType.Straight;
                    rotation = 180;
                }
                
                if (i == PointsList.Count - 1)
                {
                    type = TileType.Spawnpoint;
                }
                    
                tasks.Add(DrawRoadTile(type, currentPoint, rotation));
            }
            await Task.WhenAll(tasks);
        }
        
        private async Task DrawRoadTile(TileType type, Vector2 position, int rotation)
        {
            await 
                _mapTilesDatabase.GetTile(type)
                .InstantiateAsync(position.ToVector3WithYToZ(), Quaternion.Euler(0, rotation, 0)).Task;
        }

        public void Dispose()
        {
            _gizmoDrawerFactory.RemoveDrawer(this);
        }
    }
}