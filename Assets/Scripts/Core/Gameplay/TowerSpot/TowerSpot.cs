using Core.Services.Input;
using Lean.Touch;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TowerSpot : MonoBehaviour, IInputInteractable
{
    public bool IsInputEnabled { get; set; }

    private void Awake()
    {
        IsInputEnabled = true;
    }

    public void DoAction(LeanFinger finger)
    {
        Debug.Log("TowerSpot tapped");
    }
}