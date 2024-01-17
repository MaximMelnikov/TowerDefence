using Core;
using UnityEngine;

namespace Tools.Gizmo
{
    public class LineGizmoDrawer : MonoBehaviour, IGizmoDrawer
    {
        private ILineGizmoDrawable _drawable;

        private void OnDrawGizmos()
        {
            if (_drawable == null)
                return;

            for (var i = 0; i < _drawable.Points.Length - 1; i++)
                Gizmos.DrawLine(
                    _drawable.Points[i].ToVector3WithYToZ(),
                    _drawable.Points[i + 1].ToVector3WithYToZ());
            
            if (_drawable.IsLoop)
                Gizmos.DrawLine(
                    _drawable.Points[_drawable.Points.Length - 1].ToVector3WithYToZ(),
                    _drawable.Points[0].ToVector3WithYToZ());
        }

        public void Draw(IGizmoDrawable drawable)
        {
            _drawable = drawable as ILineGizmoDrawable;
        }
    }
}