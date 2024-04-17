using Core.Services.Input;
using Lean.Touch;
using UnityEngine;
using Zenject;

namespace Core.Gameplay
{
    [RequireComponent(typeof(SphereCollider))]
    public class TowerSpot : MonoBehaviour, IInputInteractable
    {
        private ITowersFactory _towersFactory;

        public bool IsInputEnabled { get; set; }

        [Inject]
        private void Construct(ITowersFactory towersFactory)
        {
            _towersFactory = towersFactory;
            Init();
        }

        private void Init()
        {
            IsInputEnabled = true;
        }

        public void InputAction(LeanFinger finger)
        {
            Debug.Log("TowerSpot tapped");
            _towersFactory.CreateTower("", transform.position);
            IsInputEnabled = false;
        }
    }
}