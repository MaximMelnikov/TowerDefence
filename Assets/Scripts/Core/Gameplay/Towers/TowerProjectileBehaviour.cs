using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Core.Gameplay
{
    public class TowerProjectileBehaviour : MonoBehaviour, ITowerProjectile
    {
        private Transform _target;
        private Vector3 _targetLastPosition;
        private float _speed = 2;
        private int _damage;
        private float _range;

        public void Shoot(Vector3 position)
        {
            _targetLastPosition = position;
        }

        public void Shoot(Transform target)
        {
            _target = target;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void DoDamage()
        {
            
        }

        void Update()
        {
            if (_target != null)
            {
                _targetLastPosition = _target.position;
            }

            Vector3 dir = _targetLastPosition - transform.position;
            float distanceThisFrame = _speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                DoDamage();
                Destroy();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.LookAt(_targetLastPosition);
        }
    }
}