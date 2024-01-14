using Core.StateMachine;
using UnityEngine;
using Zenject;

namespace Core.Bootstrap
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