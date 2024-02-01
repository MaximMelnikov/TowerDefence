using System.Threading.Tasks;
using Core.Gameplay.MapProceduralGenerator;
using Core.SceneLoader;
using Core.Scriptable;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.StateMachine.StateMachines.States
{
    public class GameplayPreloadState : IState
    {
        private readonly IMapGenerator _mapGenerator;
        private readonly MonstersDatabase _monstersDatabase;
        private readonly IStateMachine _projectStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public GameplayPreloadState(
            IStateMachine projectStateMachine,
            ISceneLoader sceneLoader,
            IMapGenerator mapGenerator,
            MonstersDatabase monstersDatabase)
        {
            _projectStateMachine = projectStateMachine;
            _sceneLoader = sceneLoader;
            _mapGenerator = mapGenerator;
            _monstersDatabase = monstersDatabase;
        }

        public async UniTask Enter()
        {
            Debug.Log("Enter GameplayPreloadState");
            await _mapGenerator.CreateMap(new MapSettings());
            await MonstersPreload();
            _projectStateMachine.Enter<GameplayState>();
        }

        public async UniTask Exit()
        {
            Debug.Log("Exit GameplayPreloadState");
        }
        
        private async Task MonstersPreload()
        {
            await _monstersDatabase.GetMonster(MonsterType.Capsule).LoadAssetAsync<GameObject>();
        }
    }
}