using Core.MapProceduralGenerator;
using Core.SceneLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.StateMachine.StateMachines.States
{
    public class GameplayState : IState
    {
        private readonly IMapGenerator _mapGenerator;
        private readonly ISceneLoader _sceneLoader;

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