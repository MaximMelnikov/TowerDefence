using Core.Factory.Gizmo;

namespace Core.Bootstrap.MapProceduralGenerator
{
    public class MapGenerator : IMapGenerator
    {
        private readonly IGizmoDrawerFactory _gizmoDrawerFactory;

        public MapGenerator(IGizmoDrawerFactory gizmoDrawerFactory)
        {
            _gizmoDrawerFactory = gizmoDrawerFactory;
        }
        
        public void CreateMap()
        {
            new Ellipse(_gizmoDrawerFactory, 0, 0, 1, 1);
        }
    }
}