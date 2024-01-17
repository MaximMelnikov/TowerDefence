using Core.MapProceduralGenerator;
using Tools.Gizmo;
using Zenject;

namespace Core.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
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
    }
}