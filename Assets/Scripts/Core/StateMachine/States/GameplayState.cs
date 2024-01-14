using Core.SceneLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;
using IState = Core.StateMachine.IState;

namespace Core.Gameplay
{
    public class GameplayState : IState
    {
        private readonly ISceneLoader _sceneLoader;

        public GameplayState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public UniTask Enter()
        {
            Debug.Log("Enter GameplayState");
            return new UniTask();
        }

        public UniTask Exit()
        {
            Debug.Log("Exit GameplayState");
            return new UniTask();
        }
    }
}