using System;
using System.Linq;
using System.Threading;
using Core.Gameplay.Monsters.MonstersFactory;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.Serialization;
using Zenject;

namespace Core.Gameplay.Monsters
{
    public class MonsterBehaviour : MonoBehaviour, IMonsterBehaviour
    {
        [SerializeField] private AnimationCurve _moveSpeedCurve;
        private Vector3[] _waypoints;
        private int _currentWaypointIndex;
        private IMonstersFactory _monstersFactory;

        [Inject]
        private void Construct(IMonstersFactory monstersFactory)
        {
            _monstersFactory = monstersFactory;
        }

        public void StartMove() //TODO: move to state machine
        {
            Move();
        }

        private async UniTask Move()
        {
            while (_currentWaypointIndex < _waypoints.Length)
            {
                if (Vector3.Distance(_waypoints[_currentWaypointIndex], transform.position) < 0.2f)
                {
                    _currentWaypointIndex++;
                    if (_currentWaypointIndex == _waypoints.Length)
                    {
                        _monstersFactory.DestroyMonster(this);
                        Destroy(gameObject);
                        break;
                    }
                }

                var nextWaypointPos = _waypoints[_currentWaypointIndex];
                transform.LookAt(nextWaypointPos);
                transform.Translate(0, 0, _moveSpeedCurve.Evaluate(Time.time) * Time.deltaTime);
                await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            }
        }

        public void AddWaypoints(Vector2[] waypoints)
        {
            _waypoints = new Vector3[waypoints.Length];
            for (int i = 0; i < waypoints.Length; i++)
            {
                _waypoints[i] = waypoints[i].ToVector3WithYToZ();
            }
        }
    }
}