namespace Tools.Gizmo
{
    public interface IGizmoDrawerFactory
    {
        public void CreateDrawer(IGizmoDrawable drawable);
        public void RemoveDrawer(IGizmoDrawable drawable);
    }
}