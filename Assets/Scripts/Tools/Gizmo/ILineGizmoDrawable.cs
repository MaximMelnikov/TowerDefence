using UnityEngine;

namespace Tools.Gizmo
{
    public interface ILineGizmoDrawable : IGizmoDrawable
    {
        public bool IsLoop { get; }
        public Vector2[] Points { get; }
    }
}