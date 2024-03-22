using UnityEngine;

namespace Core.Gameplay
{
    public interface ITowerProjectile
    {
        public void Shoot(Vector3 position);
        public void Shoot(Transform target);
        public void Destroy();
        public void DoDamage();
    }
}
