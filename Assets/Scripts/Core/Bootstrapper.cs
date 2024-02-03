using Core.StateMachine;
using Core.StateMachine.StateMachines.States;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private IStateMachine _projectStateMachine;

        [Inject]
        private void Construct(IStateMachine projectStateMachine)
        {
            _projectStateMachine = projectStateMachine;

            Init();
        }
        
        private void Init()
        {
            _projectStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}