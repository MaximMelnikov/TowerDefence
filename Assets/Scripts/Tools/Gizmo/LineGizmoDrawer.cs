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
                    _drawable.Points[i],
                    _drawable.Points[i + 1]);
            if (_drawable.IsLoop)
                Gizmos.DrawLine(
                    _drawable.Points[_drawable.Points.Length - 1],
                    _drawable.Points[0]);
        }

        public void Draw(IGizmoDrawable drawable)
        {
            _drawable = drawable as ILineGizmoDrawable;
        }
    }
}