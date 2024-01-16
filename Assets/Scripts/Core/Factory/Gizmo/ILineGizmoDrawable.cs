using UnityEngine;

namespace Core.Factory.Gizmo
{
    public interface ILineGizmoDrawable : IGizmoDrawable
    {
        public bool IsLoop { get; }
        public Vector2[] Points { get; }
    }
}