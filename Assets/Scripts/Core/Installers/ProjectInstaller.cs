using Core.Factory;
using Core.SceneLoader;
using Core.StateMachine;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindProjectStateMachine();
            BindProjectStarterFactory();
            BindSceneLoader();
        }
        
        private void BindProjectStateMachine()
        {
            Container
                .Bind<IStateMachine>()
                .To<ProjectStateMachine>()
                .AsSingle();
        }
        private void BindProjectStarterFactory()
        {
            Container
                .Bind<BootstrapperFactory>()
                .AsSingle();
        }
        
        private void BindSceneLoader()
        {
            Container
                .Bind<ISceneLoader>()
                .To<SceneLoader.SceneLoader>()
                .AsSingle();
        }
        
    }
}