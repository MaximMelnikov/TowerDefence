using System;
using Core.Factory.Gizmo;
using UnityEngine;

namespace Core.Bootstrap.MapProceduralGenerator
{
    public class MapRoad : IDisposable, ILineGizmoDrawable
    {
        private readonly IGizmoDrawerFactory _gizmoDrawerFactory;
        public int Segment { get; }
        public Ellipse Destination { get; }
        
        public bool IsLoop { get => false; }
        public Vector2[] Points { get; private set; }
        
        public MapRoad(IGizmoDrawerFactory gizmoDrawerFactory, int segment, Ellipse destinationEllipse)
        {
            _gizmoDrawerFactory = gizmoDrawerFactory;
            Segment = segment;
            Destination = destinationEllipse;

            SetWaypoints();
        }

        private void SetWaypoints()
        {
            Points = new Vector2[2];
            Points[0] = new Vector2(0, 0);
            Points[1] = Destination.Points[Segment];
            
            _gizmoDrawerFactory.CreateDrawer(this);
        }

        public void Dispose()
        {
            _gizmoDrawerFactory.RemoveDrawer(this);
        }

        
    }
}