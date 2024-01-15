using Core.Bootstrap.MapProceduralGenerator;
using Core.SceneLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;
using IState = Core.StateMachine.IState;

namespace Core.Gameplay
{
    public class GameplayState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IMapGenerator _mapGenerator;

        public GameplayState(
            ISceneLoader sceneLoader,
            IMapGenerator mapGenerator)
        {
            _sceneLoader = sceneLoader;
            _mapGenerator = mapGenerator;
        }

        public UniTask Enter()
        {
            Debug.Log("Enter GameplayState");
            _mapGenerator.CreateMap();
            return new UniTask();
        }

        public UniTask Exit()
        {
            Debug.Log("Exit GameplayState");
            return new UniTask();
        }
    }
}