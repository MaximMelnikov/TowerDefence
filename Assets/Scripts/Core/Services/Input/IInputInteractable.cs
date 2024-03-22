using Lean.Touch;

namespace Core.Services.Input
{
    public interface IInputInteractable
    {
        public bool IsInputEnabled { get; set; }
        public void InputAction(LeanFinger finger);
    }
}