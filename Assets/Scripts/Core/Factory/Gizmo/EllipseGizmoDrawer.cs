using Core.Factory;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Bootstrap.MapProceduralGenerator
{
    public class EllipseGizmoDrawer : MonoBehaviour, Factory.IGizmoDrawer
    {
        public Ellipse _ellipse;
        private void OnDrawGizmos()
        {
            if (_ellipse == null)
                return;

            for (int i = 0; i < _ellipse.EllipsePoints.Length-1; i++)
            {
                Gizmos.DrawLine(
                    _ellipse.EllipsePoints[i], 
                    _ellipse.EllipsePoints[i+1]);
            }
            Gizmos.DrawLine(
                _ellipse.EllipsePoints[_ellipse.EllipsePoints.Length-1], 
                _ellipse.EllipsePoints[0]);
        }

        public void Draw(IGizmoDrawable drawable)
        {
            _ellipse = drawable as Ellipse;
        }
    }
}