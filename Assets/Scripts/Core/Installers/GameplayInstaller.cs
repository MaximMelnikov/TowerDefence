using Core.Bootstrap.MapProceduralGenerator;
using Core.Factory;
using Core.Factory.Gizmo;
using Zenject;

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