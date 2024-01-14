using Cysharp.Threading.Tasks;

namespace Core.StateMachine
{
    public interface IStateMachine
    {
        public IState CurrentState { get; }
        public void RegisterState<TState>(IState state) where TState : IState;
        public UniTask Enter<TState>() where TState : class, IState;
    }
}