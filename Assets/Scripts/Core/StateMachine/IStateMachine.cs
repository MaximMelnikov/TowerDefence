using System;
using Cysharp.Threading.Tasks;

namespace Core.StateMachine
{
    public interface IStateMachine
    {
        public Action<IState> OnStateChange { get; set; }
        public IState CurrentState { get; }
        public void RegisterState<TState>(IState state) where TState : IState;
        public UniTask Enter<TState>(bool force = false) where TState : class, IState;
    }
}