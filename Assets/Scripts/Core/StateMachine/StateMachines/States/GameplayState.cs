using Core.SceneLoader;
using Core.Services.MapProceduralGenerator;
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

        public async UniTask Enter()
        {
            Debug.Log("Enter GameplayState");
            await _mapGenerator.CreateMap(new MapSettings());
        }

        public async UniTask Exit()
        {
            Debug.Log("Exit GameplayState");
        }
    }
}