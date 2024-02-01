using Core.Gameplay.MapProceduralGenerator;
using Core.SceneLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.StateMachine.StateMachines.States
{
    public class GameplayPreloadState : IState
    {
        private readonly IMapGenerator _mapGenerator;
        private readonly IStateMachine _projectStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public GameplayPreloadState(
            IStateMachine projectStateMachine,
            ISceneLoader sceneLoader,
            IMapGenerator mapGenerator)
        {
            _projectStateMachine = projectStateMachine;
            _sceneLoader = sceneLoader;
            _mapGenerator = mapGenerator;
        }

        public async UniTask Enter()
        {
            Debug.Log("Enter GameplayPreloadState");
            await _mapGenerator.CreateMap(new MapSettings());
            _projectStateMachine.Enter<GameplayState>();
        }

        public async UniTask Exit()
        {
            Debug.Log("Exit GameplayPreloadState");
        }
    }
}