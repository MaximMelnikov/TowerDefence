using UnityEngine;

namespace Core.Factory.Gizmo
{
    public class CircleGizmoDrawer : MonoBehaviour, IGizmoDrawer
    {
        private ICircleGizmoDrawable _drawable;
        private void OnDrawGizmos()
        {
            if (_drawable == null)
                return;

            Gizmos.DrawSphere(
                _drawable.CircleDrawablePosition,
                0.1f);
        }

        public void Draw(IGizmoDrawable drawable)
        {
            _drawable = drawable as ICircleGizmoDrawable;
        }
    }
}