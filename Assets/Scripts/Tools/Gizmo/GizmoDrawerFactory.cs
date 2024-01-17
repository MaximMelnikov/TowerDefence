using System.Collections.Generic;
using UnityEngine;

namespace Tools.Gizmo
{
    public class GizmoDrawerFactory : IGizmoDrawerFactory
    {
        private readonly GameObject _container;
        private readonly Dictionary<IGizmoDrawable, IGizmoDrawer> componentsList;

        public GizmoDrawerFactory()
        {
            componentsList = new Dictionary<IGizmoDrawable, IGizmoDrawer>();
            _container = new GameObject("GizmoContainer");
        }

        public void CreateDrawer(IGizmoDrawable drawable)
        {
            IGizmoDrawer drawer = null;
            if (drawable is ILineGizmoDrawable) drawer = _container.AddComponent<LineGizmoDrawer>();
            componentsList.Add(drawable, drawer);

            drawer.Draw(drawable);
        }

        public void RemoveDrawer(IGizmoDrawable drawable)
        {
            Object.Destroy(drawable as MonoBehaviour);
            componentsList.Remove(drawable);
        }
    }
}