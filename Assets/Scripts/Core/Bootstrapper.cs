using Core.StateMachine;
using Core.StateMachine.StateMachines.States;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        private IStateMachine projectStateMachine;

        [Inject]
        private void Construct(IStateMachine projectStateMachine)
        {
            this.projectStateMachine = projectStateMachine;

            Init();
        }
        
        private void Init()
        {
            projectStateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}