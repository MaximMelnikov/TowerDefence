namespace Core.Services.Input
{
    public interface IInputService
    {
        public void BindInputs();
        public void UnbindInputs();
        public void EnableInput();
        public void DisableInput();
    }
}
