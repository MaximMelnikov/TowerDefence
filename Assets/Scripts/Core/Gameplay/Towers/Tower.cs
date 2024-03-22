using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Gameplay
{
    public class Tower : MonoBehaviour, ITower
    {
        [SerializeField]
        private AssetReference _towerProjectile;

        private bool _isFiring;
        private TowerSettings _settings;
        private CancellationTokenSource _firingCancellationToken;

        private void Start()
        {
            StartFiring();
        }

        public async UniTask StartFiring()
        {
            _isFiring = true;
            _firingCancellationToken = new CancellationTokenSource();

            while (_isFiring)
            {
                await UniTask.WaitForSeconds(_settings._fireRate, cancellationToken: _firingCancellationToken.Token);
            }
        }

        public void StopFiring()
        {
            _isFiring = false;
            _firingCancellationToken.Cancel();
            _firingCancellationToken.Dispose();
        }

        private void OnDestroy()
        {
            _firingCancellationToken.Cancel();
            _firingCancellationToken.Dispose();
        }
    }
}
