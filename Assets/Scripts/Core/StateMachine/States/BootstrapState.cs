using Core.SceneLoader;
using Core.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Bootstrap
{
    public class BootstrapState : IState
    {
        private const string GameplayLevelName = "Gameplay";
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        public async UniTask Enter()
        {
            Debug.Log("Enter BootstrapState");
            //You can show loading screen here and init services
            await _sceneLoader.Load(GameplayLevelName);
        }

        public async UniTask Exit()
        {
            Debug.Log("Exit BootstrapState");
        }
    }
}