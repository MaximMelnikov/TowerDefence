using Lean.Touch;
using UnityEngine;

public class InputService : IInputService
{
    public InputService()
    {
        Debug.Log("InputService");
        BindInputs();
    }

    public void BindInputs()
    {
        LeanTouch.OnFingerTap += HandleFingerTap;
    }

    public void DisableInput()
    {
        throw new System.NotImplementedException();
    }

    public void EnableInput()
    {
        throw new System.NotImplementedException();
    }

    public void UnbindInputs()
    {
        throw new System.NotImplementedException();
    }

    public void HandleFingerTap(LeanFinger finger)
    {
        if (!finger.IsOverGui)
        {
            Debug.Log("Finger " + finger.Index + " tapped the screen");
        }
    }
}