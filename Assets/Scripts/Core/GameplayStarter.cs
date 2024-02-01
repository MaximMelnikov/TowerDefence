using Core.Factory;
using Core.Gameplay.MapProceduralGenerator;
using Core.SceneLoader;
using Core.Scriptable;
using Core.StateMachine;
using Core.StateMachine.StateMachines.States;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GameplayStarter : MonoBehaviour
    {
        private IStateMachine _projectStateMachine;
        private BootstrapperFactory _bootstrapperFactory;
        private ISceneLoader _sceneLoader;
        private IMapGenerator _mapGenerator;
        private MonstersDatabase _monstersDatabase;

        [Inject]
        private void Construct(
            IStateMachine projectStateMachine, 
            BootstrapperFactory bootstrapperFactory,
            ISceneLoader sceneLoader,
            IMapGenerator mapGenerator,
            MonstersDatabase monstersDatabase)
        {
            _projectStateMachine = projectStateMachine;
            _bootstrapperFactory = bootstrapperFactory;
            _sceneLoader = sceneLoader;
            _mapGenerator = mapGenerator;
            _monstersDatabase = monstersDatabase;
        }
        
        private void Awake()
        {
            var projectStarter = FindObjectOfType<Bootstrapper>();
      
            if(projectStarter != null) return;

            _bootstrapperFactory.CreateBootstrapper();
        }

        private void Start()
        {
            _projectStateMachine.RegisterState<GameplayPreloadState>(new GameplayPreloadState(_projectStateMachine, _sceneLoader, _mapGenerator, _monstersDatabase));
            _projectStateMachine.RegisterState<GameplayState>(new GameplayState(_projectStateMachine, _sceneLoader, _mapGenerator));
            _projectStateMachine.Enter<GameplayPreloadState>();
        }
    }
}