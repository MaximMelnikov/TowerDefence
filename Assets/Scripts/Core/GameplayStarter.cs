using Core.Factory;
using Core.SceneLoader;
using Core.Services.MapProceduralGenerator;
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

        [Inject]
        private void Construct(
            IStateMachine projectStateMachine, 
            BootstrapperFactory bootstrapperFactory,
            ISceneLoader sceneLoader,
            IMapGenerator mapGenerator)
        {
            _projectStateMachine = projectStateMachine;
            _bootstrapperFactory = bootstrapperFactory;
            _sceneLoader = sceneLoader;
            _mapGenerator = mapGenerator;
        }
        
        private void Awake()
        {
            var projectStarter = FindObjectOfType<Bootstrapper>();
      
            if(projectStarter != null) return;

            _bootstrapperFactory.CreateBootstrapper();
        }

        private void Start()
        {
            _projectStateMachine.RegisterState<GameplayState>(new GameplayState(_sceneLoader, _mapGenerator));
            _projectStateMachine.Enter<GameplayState>();
        }
    }
}