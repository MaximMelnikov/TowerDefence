using Core.Gameplay.Monsters.MonstersFactory;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Core.Gameplay
{
    public class Tower : MonoBehaviour, ITower
    {
        [SerializeField]
        private AssetReference _towerProjectile;

        private bool _isFiring;
        private TowerSettings _settings;
        private CancellationTokenSource _firingCancellationToken;
        private IMonstersFactory _monstersFactory;
        
        [Inject]
        private void Construct(IMonstersFactory monstersFactory)
        {
            _monstersFactory = monstersFactory;
            Init();
        }

        private void Init()
        {
            _settings = new TowerSettings();
            PreloadProjectile();
            StartFiring();
        }

        private async UniTask PreloadProjectile()
        {
            await _towerProjectile.LoadAssetAsync<GameObject>();
        }

        public async UniTask StartFiring()
        {
            _isFiring = true;
            _firingCancellationToken = new CancellationTokenSource();

            while (_isFiring)
            {
                Fire();
                await UniTask.WaitForSeconds(_settings._fireRate, cancellationToken: _firingCancellationToken.Token);
            }
        }

        private Transform GetClosestTarget()
        {
            var monsters = _monstersFactory.GetMonsters();
            Transform closestTarget = null;
            float closestTargetDistance = float.MaxValue;
            foreach (var item in monsters)
            {
                if (item is MonoBehaviour targetMonoBehaviour)
                {
                    var distance = Vector3.Distance(transform.position, targetMonoBehaviour.transform.position);
                    if (distance < _settings._fireDistance && distance < closestTargetDistance)
                    {
                        closestTargetDistance = distance;
                        closestTarget = targetMonoBehaviour.transform;
                    }
                }
            }

            return closestTarget;
        }

        private async UniTask Fire()
        {
            var closestTarget = GetClosestTarget();
            if (!closestTarget)
                return;

            var projectile = await _towerProjectile.InstantiateAsync(transform.position + new Vector3(0,1,0), Quaternion.identity);
            var towerProjectileBehaviour = projectile.GetComponent<TowerProjectileBehaviour>();
            towerProjectileBehaviour.Shoot(closestTarget);
        }

        public void StopFiring()
        {
            _isFiring = false;
            _firingCancellationToken?.Cancel();
            _firingCancellationToken?.Dispose();
        }

        private void OnDestroy()
        {
            _firingCancellationToken?.Cancel();
            _firingCancellationToken?.Dispose();
        }
    }
}
