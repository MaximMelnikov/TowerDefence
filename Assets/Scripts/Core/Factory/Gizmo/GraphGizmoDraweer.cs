using UnityEngine;

namespace Core.Factory.Gizmo
{
    public class GraphGizmoDraweer : MonoBehaviour, Factory.IGizmoDrawer
    {
        private IGraphGizmoDrawable _drawable;
        private void OnDrawGizmos()
        {
            if (_drawable == null)
                return;

            for (int i = 0; i < _drawable.Edges.Length; i++)
            {
                Gizmos.DrawLine(
                    _drawable.Edges[i].Item1, 
                    _drawable.Edges[i].Item2);
            }
        }

        public void Draw(IGizmoDrawable drawable)
        {
            _drawable = drawable as IGraphGizmoDrawable;
        }
    }
}