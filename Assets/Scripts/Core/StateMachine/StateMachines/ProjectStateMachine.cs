using Core.SceneLoader;
using Core.StateMachine.StateMachines.States;

namespace Core.StateMachine.StateMachines
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