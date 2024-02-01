using System.Collections;
using Core.Scriptable;
using Core.StateMachine;
using Core.StateMachine.StateMachines.States;
using UnityEngine;
using Zenject;

namespace Core.Gameplay
{
    public class Spawner : MonoBehaviour
    {
        private IStateMachine _projectStateMachine;
        private MonstersDatabase _monstersDatabase;
        private SpawnerBalance[] _spawnerBalance;
        private IEnumerator _spawnerLiveCycle;
        [SerializeField] private Transform _spawnPoint;
        
        [Inject]
        private void Construct(IStateMachine projectStateMachine, MonstersDatabase monstersDatabase)
        {
            _projectStateMachine = projectStateMachine;
            _monstersDatabase = monstersDatabase;
            Init();
        }

        private void Init()
        {
            _projectStateMachine.OnStateChange += OnStateChange;
            _spawnerLiveCycle = SpawnerLiveCycle();
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
                StartCoroutine(_spawnerLiveCycle);
            }
        }
        
        IEnumerator SpawnerLiveCycle()
        {
            for (int i = 0; i < _spawnerBalance.Length; i++)
            {
                for (int j = 0; j < _spawnerBalance[i].CapsuleCount; j++)
                {
                    SpawnMonster(MonsterType.Capsule);
                    yield return new WaitForSeconds(1f);
                }
                for (int j = 0; j < _spawnerBalance[i].SphereCount; j++)
                {
                    SpawnMonster(MonsterType.Sphere);
                    yield return new WaitForSeconds(1f);
                }
                for (int j = 0; j < _spawnerBalance[i].BoxCount; j++)
                {
                    SpawnMonster(MonsterType.Box);
                    yield return new WaitForSeconds(1f);
                }
                
                yield return new WaitForSeconds(_spawnerBalance[i].NextStageDelay);
            }
        }

        private void SpawnMonster(MonsterType monsterType)
        {
            //TODO: add monsters factory
            var monsterAsset =_monstersDatabase.GetMonster(monsterType);
            if (monsterAsset == null)
            {
                Debug.LogError($"MonsterAsset {monsterType} not found");
                return;
            }
            monsterAsset.InstantiateAsync(_spawnPoint.position, Quaternion.identity);
        }
    }
}