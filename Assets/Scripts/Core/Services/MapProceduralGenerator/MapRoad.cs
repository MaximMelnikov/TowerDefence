using System;
using System.Collections.Generic;
using Core.Services.MapProceduralGenerator.MapFactory;
using Cysharp.Threading.Tasks;
using Tools.Gizmo;
using UnityEngine;

namespace Core.Services.MapProceduralGenerator
{
    public class MapRoad : IDisposable, ILineGizmoDrawable
    {
        private readonly IGizmoDrawerFactory _gizmoDrawerFactory;
        private readonly IMapFactory _mapFactory;

        public bool LineGizmoIsLoop { get => false; }
        public Vector2[] LineGizmoPoints { get; private set; }
        public List<Vector2> Waypoints = new List<Vector2>();
        
        public MapRoad(
            IGizmoDrawerFactory gizmoDrawerFactory, 
            IMapFactory mapFactory)
        {
            _gizmoDrawerFactory = gizmoDrawerFactory;
            _mapFactory = mapFactory;

            _gizmoDrawerFactory.CreateDrawer(this);
        }

        public void AddWaypoint(Vector2 position)
        {
            Waypoints.Add(position);
            LineGizmoPoints = Waypoints.ToArray();
        }

        public async UniTask DrawRoad()
        {
            List<UniTask> tasks = new List<UniTask>(Waypoints.Count);
            tasks.Add(_mapFactory.CreateObject(TileType.Crossroad, Waypoints[0], 0));
            for (int i = 1; i < Waypoints.Count; i++)
            {
                var currentPoint = Waypoints[i];
                
                var previousPoint = Waypoints[i - 1];
                var previousPointDirection = currentPoint - previousPoint;

                Vector2 nextPoint;

                if (i == Waypoints.Count - 1)
                {
                    nextPoint = currentPoint + previousPointDirection;
                }
                else
                {
                    nextPoint = Waypoints[i + 1];
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
                else if (nextPointDirection == Vector2.up && previousPointDirection == Vector2.right
                         || nextPointDirection == Vector2.right && previousPointDirection == Vector2.up)
                {
                    type = TileType.Turn;
                    rotation = 270;
                }
                else if (nextPointDirection == Vector2.up && previousPointDirection == Vector2.left
                         || nextPointDirection == Vector2.left && previousPointDirection == Vector2.up)
                {
                    type = TileType.Turn;
                    rotation = 180;
                }
                else if (nextPointDirection == Vector2.down && previousPointDirection == Vector2.right
                         || nextPointDirection == Vector2.right && previousPointDirection == Vector2.down)
                {
                    type = TileType.Turn;
                    rotation = 0;
                }
                else if (nextPointDirection == Vector2.down && previousPointDirection == Vector2.left
                         || nextPointDirection == Vector2.left && previousPointDirection == Vector2.down)
                {
                    type = TileType.Turn;
                    rotation = 90;
                }
                
                if (i == Waypoints.Count - 1)
                {
                    type = TileType.Spawnpoint;
                }
                    
                tasks.Add(_mapFactory.CreateObject(type, currentPoint, rotation));
            }
            await UniTask.WhenAll(tasks);
        }

        public void Dispose()
        {
            _gizmoDrawerFactory.RemoveDrawer(this);
        }
    }
}