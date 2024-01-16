using UnityEngine;

namespace Core.Factory.Gizmo
{
    public interface IGraphGizmoDrawable : IGizmoDrawable
    {
        public (Vector2, Vector2)[] Edges { get; }
    }
}