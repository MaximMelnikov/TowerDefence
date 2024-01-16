using UnityEngine;

namespace Core.Factory.Gizmo
{
    public interface ICircleGizmoDrawable : IGizmoDrawable
    {
        public Vector2 CircleDrawablePosition { get; }
    }
}