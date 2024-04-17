using System.Collections.Generic;
using Core.Scriptable;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.Gameplay.Monsters.MonstersFactory
{
    public class MonstersFactory : IMonstersFactory
    {
        private DiContainer _container;
        private readonly MonstersDatabase _monstersDatabase;
        private readonly Transform _monstersContainer;
        private List<IMonsterBehaviour> _monsters = new List<IMonsterBehaviour>();

        public MonstersFactory(DiContainer container, MonstersDatabase monstersDatabase)
        {
            _container = container;
            _monstersDatabase = monstersDatabase;
            _monstersContainer = new GameObject("MonstersContainer").transform;
        }
        
        public async UniTask<IMonsterBehaviour> CreateMonster(MonsterType type, Vector3 position, Vector2[] waypoints)
        {
            var monsterAsset =_monstersDatabase.GetMonster(type);
            if (monsterAsset == null)
            {
                Debug.LogError($"MonsterAsset {type} not found");
                return null;
            }
            var monster = await monsterAsset.InstantiateAsync(position, Quaternion.identity, _monstersContainer).Task;
            _container.InjectGameObject(monster);
            var monsterBehaviour = monster.GetComponent<IMonsterBehaviour>();
            monsterBehaviour.AddWaypoints(waypoints);
            monsterBehaviour.StartMove();
            _monsters.Add(monsterBehaviour);
            return monsterBehaviour;
        }

        public List<IMonsterBehaviour> GetMonsters()
        {
            return _monsters;
        }

        public void DestroyMonster(IMonsterBehaviour monster)
        {
            _monsters.Remove(monster);
        }

        public void Reset()
        {
            foreach (var monster in _monsters)
            {
                GameObject.Destroy((monster as MonoBehaviour).gameObject);
            }
            _monsters.Clear();
        }
    }
}