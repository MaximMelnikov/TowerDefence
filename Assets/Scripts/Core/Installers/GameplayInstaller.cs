using Core.Services.MapProceduralGenerator;
using Core.Services.MapProceduralGenerator.MapFactory;
using Tools.Gizmo;
using Zenject;

namespace Core.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMapTilesDatabase();
            BindMapFactory();
            BindGizmoDrawerFactory();
            BindMapGenerator();
        }

        private void BindGizmoDrawerFactory()
        {
            Container
                .Bind<IGizmoDrawerFactory>()
                .To<GizmoDrawerFactory>()
                .AsSingle();
        }
    
        private void BindMapGenerator()
        {
            Container
                .Bind<IMapGenerator>()
                .To<MapGenerator>()
                .AsSingle();
        }
        
        private void BindMapTilesDatabase()
        {
            Container
                .Bind<MapTilesDatabase>()
                .FromResources("MapTilesDatabase");
        }
        
        private void BindMapFactory()
        {
            Container
                .Bind<IMapFactory>()
                .To<MapFactory>()
                .AsSingle();
        }
    }
}