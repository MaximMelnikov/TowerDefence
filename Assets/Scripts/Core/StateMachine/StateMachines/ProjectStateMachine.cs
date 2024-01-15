using Core.Bootstrap;
using Core.Bootstrap.MapProceduralGenerator;
using Core.Gameplay;
using Core.SceneLoader;

namespace Core.StateMachine
{
    public class ProjectStateMachine : StateMachine
    {
        private readonly ISceneLoader _sceneLoader;

        public ProjectStateMachine(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;

            RegisterState<BootstrapState>(new BootstrapState(sceneLoader));
        }
    }
}