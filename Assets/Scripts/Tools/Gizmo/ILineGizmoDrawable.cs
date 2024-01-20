using UnityEngine;

namespace Tools.Gizmo
{
    public interface ILineGizmoDrawable : IGizmoDrawable
    {
        public bool LineGizmoIsLoop { get; }
        public Vector2[] LineGizmoPoints { get; }
    }
}