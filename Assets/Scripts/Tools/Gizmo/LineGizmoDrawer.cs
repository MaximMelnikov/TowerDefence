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

            for (var i = 0; i < _drawable.LineGizmoPoints.Length - 1; i++)
                Gizmos.DrawLine(
                    _drawable.LineGizmoPoints[i].ToVector3WithYToZ(),
                    _drawable.LineGizmoPoints[i + 1].ToVector3WithYToZ());
            
            if (_drawable.LineGizmoIsLoop)
                Gizmos.DrawLine(
                    _drawable.LineGizmoPoints[_drawable.LineGizmoPoints.Length - 1].ToVector3WithYToZ(),
                    _drawable.LineGizmoPoints[0].ToVector3WithYToZ());
        }

        public void Draw(IGizmoDrawable drawable)
        {
            _drawable = drawable as ILineGizmoDrawable;
        }
    }
}