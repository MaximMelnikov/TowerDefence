using Core.Factory;
using Core.Factory.Gizmo;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Bootstrap.MapProceduralGenerator
{
    public class LineGizmoDrawer : MonoBehaviour, Factory.IGizmoDrawer
    {
        private ILineGizmoDrawable _drawable;
        private void OnDrawGizmos()
        {
            if (_drawable == null)
                return;

            for (int i = 0; i < _drawable.Points.Length-1; i++)
            {
                Gizmos.DrawLine(
                    _drawable.Points[i], 
                    _drawable.Points[i+1]);
            }
            Gizmos.DrawLine(
                _drawable.Points[_drawable.Points.Length-1], 
                _drawable.Points[0]);
        }

        public void Draw(IGizmoDrawable drawable)
        {
            _drawable = drawable as ILineGizmoDrawable;
        }
    }
}