using Cysharp.Threading.Tasks;

namespace Core.StateMachine
{
    public interface IState
    {
        public UniTask Enter();
        public UniTask Exit();
    }
}