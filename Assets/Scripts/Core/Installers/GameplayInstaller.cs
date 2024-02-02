using Core.Gameplay.MapProceduralGenerator;
using Core.Gameplay.MapProceduralGenerator.MapFactory;
using Core.Gameplay.Monsters.MonstersFactory;
using Core.Scriptable;
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
            BindMonstersDatabase();
            BindMonstersFactory();
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
        
        private void BindMonstersDatabase()
        {
            Container
                .Bind<MonstersDatabase>()
                .FromResources("MonstersDatabase");
        }
        
        private void BindMonstersFactory()
        {
            Container
                .Bind<IMonstersFactory>()
                .To<MonstersFactory>()
                .AsSingle();
        }
    }
}