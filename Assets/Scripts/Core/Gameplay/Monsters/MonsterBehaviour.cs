using System;
using UnityEngine;

namespace Core.Gameplay.Monsters
{
    public class MonsterBehaviour : MonoBehaviour, IMonsterBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        private void Update()
        {
            transform.position += Vector3.left * _curve.Evaluate(Time.time) * Time.deltaTime;
        }
    }
}