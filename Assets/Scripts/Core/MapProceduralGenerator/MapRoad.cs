using System;
using System.Collections.Generic;
using Tools.Gizmo;
using UnityEngine;

namespace Core.MapProceduralGenerator
{
    public class MapRoad : IDisposable, ILineGizmoDrawable
    {
        private readonly IGizmoDrawerFactory _gizmoDrawerFactory;
        private readonly int _spawnpointsOnRoadCount;
        
        public bool IsLoop { get => false; }
        public Vector2[] Points { get; private set; }
        public List<Vector2> PointsList = new List<Vector2>();
        
        public MapRoad(IGizmoDrawerFactory gizmoDrawerFactory, int spawnpointsOnRoadCount)
        {
            _gizmoDrawerFactory = gizmoDrawerFactory;
            _spawnpointsOnRoadCount = spawnpointsOnRoadCount;

            _gizmoDrawerFactory.CreateDrawer(this);
        }

        public void SetWaypoints(Vector2 position)
        {
            PointsList.Add(position);
            Points = PointsList.ToArray();
        }

        public void Dispose()
        {
            _gizmoDrawerFactory.RemoveDrawer(this);
        }
    }
}