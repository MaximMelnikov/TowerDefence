using System;
using System.Linq;
using System.Threading;
using Core.Gameplay.MapProceduralGenerator;
using Core.Gameplay.Monsters;
using Core.Gameplay.Monsters.MonstersFactory;
using Core.Scriptable;
using Core.StateMachine;
using Core.StateMachine.StateMachines.States;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using IState = Core.StateMachine.IState;

namespace Core.Gameplay
{
    public class Spawner : MonoBehaviour
    {
        private IStateMachine _projectStateMachine;
        private IMonstersFactory _monstersFactory;
        private MapRoad _mapRoad;
        private SpawnerBalance[] _spawnerBalance;
        [SerializeField] private Transform _spawnPoint;
        
        [Inject]
        private void Construct(IStateMachine projectStateMachine, IMonstersFactory monstersFactory)
        {
            _projectStateMachine = projectStateMachine;
            _monstersFactory = monstersFactory;
            Init();
        }

        public void AddRoad(MapRoad mapRoad)
        {
            _mapRoad = mapRoad;
        }

        private void Init()
        {
            _projectStateMachine.OnStateChange += OnStateChange;
            LoadBalance();
        }

        private void LoadBalance()
        {
            //temp balance
            _spawnerBalance = new SpawnerBalance[3];
            _spawnerBalance[0] = new SpawnerBalance(3, 0, 0, 3);
            _spawnerBalance[1] = new SpawnerBalance(4, 0, 0, 5);
            _spawnerBalance[2] = new SpawnerBalance(5, 0, 0, 5);
        }

        private void OnStateChange(IState obj)
        {
            if (obj is GameplayState)
            {
                SpawnerLiveCycle();
            }
        }
        
        private async UniTask SpawnerLiveCycle()
        {
            for (int i = 0; i < _spawnerBalance.Length; i++)
            {
                for (int j = 0; j < _spawnerBalance[i].CapsuleCount; j++)
                {
                    SpawnMonster(MonsterType.Capsule);
                    await UniTask.WaitForSeconds(1f, cancellationToken: this.GetCancellationTokenOnDestroy());
                }
                for (int j = 0; j < _spawnerBalance[i].SphereCount; j++)
                {
                    SpawnMonster(MonsterType.Sphere);
                    await UniTask.WaitForSeconds(1f, cancellationToken: this.GetCancellationTokenOnDestroy());
                }
                for (int j = 0; j < _spawnerBalance[i].BoxCount; j++)
                {
                    SpawnMonster(MonsterType.Box);
                    await UniTask.WaitForSeconds(1f, cancellationToken: this.GetCancellationTokenOnDestroy());
                }
                
                await UniTask.WaitForSeconds(_spawnerBalance[i].NextStageDelay, cancellationToken: this.GetCancellationTokenOnDestroy());
            }
        }

        private async UniTask SpawnMonster(MonsterType monsterType)
        {
            var waypoints = _mapRoad.Waypoints.ToArray();
            
            _monstersFactory.CreateMonster(monsterType, _spawnPoint.position, waypoints.Reverse().ToArray());
        }
        
        private void OnDestroy()
        {
            _projectStateMachine.OnStateChange -= OnStateChange;
        }
    }
}