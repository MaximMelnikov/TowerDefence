using UnityEngine;

namespace Core.Services.Input
{
    public interface IInputTrigger
    {
        public bool IsTriggered(Vector2 inputPoint);
    }
}