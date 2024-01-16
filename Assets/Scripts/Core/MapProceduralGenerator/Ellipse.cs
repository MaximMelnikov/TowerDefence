using System;
using Core.Factory;
using Core.Factory.Gizmo;
using UnityEngine;

namespace Core.Bootstrap.MapProceduralGenerator
{
    public class Ellipse : IDisposable, ILineGizmoDrawable
    {
        private const int Segments = 12;
        
        private readonly IGizmoDrawerFactory _gizmoDrawerFactory;
        private readonly float _xPos;
        private readonly float _yPos;
        private readonly float _xScale;
        private readonly float _yScale;
        
        public bool IsLoop { get => true; }
        public Vector2[] Points { get; private set; }

        public Ellipse(IGizmoDrawerFactory gizmoDrawerFactory, float xPos, float yPos, float xScale, float yScale)
        {
            _gizmoDrawerFactory = gizmoDrawerFactory;
            _xPos = xPos;
            _yPos = yPos;
            _xScale = xScale;
            _yScale = yScale;

            CalculateEllipsePoints();
            _gizmoDrawerFactory.CreateDrawer(this);
        }

        private void CalculateEllipsePoints()
        {
            Points = new Vector2[Segments];
            
            for (int i = 0; i < Segments; i++)
            {
                float angle = ((float)i / (float)Segments) * 360 * Mathf.Deg2Rad;
                float x = Mathf.Sin(angle) * _xScale + _xPos;
                float y = Mathf.Cos(angle) * _yScale + _yPos;

                Points[i] = new Vector2(x, y);
            }
        }

        public void Dispose()
        {
            _gizmoDrawerFactory.RemoveDrawer(this);
        }
    }
}