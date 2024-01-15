using System;
using Core.Factory;
using Core.Factory.Gizmo;
using UnityEngine;

namespace Core.Bootstrap.MapProceduralGenerator
{
    public class Ellipse : IDisposable, IGizmoDrawable
    {
        private const int Segments = 30;
        
        private readonly IGizmoDrawerFactory _gizmoDrawerFactory;
        private readonly float _xPos;
        private readonly float _yPos;
        private readonly float _xScale;
        private readonly float _yScale;
        
        public Vector2[] EllipsePoints { get; private set; }

        public Ellipse(IGizmoDrawerFactory gizmoDrawerFactory,float xPos, float yPos, float xScale, float yScale)
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
            EllipsePoints = new Vector2[Segments];
            
            for (int i = 0; i < Segments; i++)
            {
                float angle = ((float)i / (float)Segments) * 360 * Mathf.Deg2Rad;
                float x = Mathf.Sin(angle) * _xScale + _xPos;
                float y = Mathf.Cos(angle) * _yScale + _yPos;

                EllipsePoints[i] = new Vector2(x, y);
            }
        }

        public void Dispose()
        {
            _gizmoDrawerFactory.RemoveDrawer(this);
        }
    }
}