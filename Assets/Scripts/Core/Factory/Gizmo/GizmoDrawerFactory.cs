using System;
using System.Collections.Generic;
using Core.Bootstrap.MapProceduralGenerator;
using Core.Factory.Gizmo;
using UnityEditor;
using UnityEngine;

namespace Core.Factory
{
    public class GizmoDrawerFactory : IGizmoDrawerFactory
    {
        private readonly GameObject _container;
        private Dictionary<IGizmoDrawable, IGizmoDrawer> componentsList;

        public GizmoDrawerFactory()
        {
            componentsList = new Dictionary<IGizmoDrawable, IGizmoDrawer>();
            _container = new GameObject("GizmoContainer");
        }
        
        public void CreateDrawer(IGizmoDrawable drawable)
        {
            IGizmoDrawer drawer = null;
            if (drawable is ILineGizmoDrawable)
            {
                drawer = _container.AddComponent<LineGizmoDrawer>();
            }
            else if (drawable is IGraphGizmoDrawable)
            {
                drawer = _container.AddComponent<GraphGizmoDraweer>();
            }
            else if (drawable is ICircleGizmoDrawable)
            {
                drawer = _container.AddComponent<CircleGizmoDrawer>();
            }
            componentsList.Add(drawable, drawer);
            
            drawer.Draw(drawable);
        }

        public void RemoveDrawer(IGizmoDrawable drawable)
        {
            GameObject.Destroy(drawable as MonoBehaviour);
            componentsList.Remove(drawable);
        }
    }
}