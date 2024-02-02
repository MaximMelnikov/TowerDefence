using System.Collections.Generic;
using Core.Scriptable;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Gameplay.Monsters.MonstersFactory
{
    public class MonstersFactory : IMonstersFactory
    {
        private readonly MonstersDatabase _monstersDatabase;
        private readonly Transform _monstersContainer;
        private List<GameObject> _monsters = new List<GameObject>();

        public MonstersFactory(MonstersDatabase monstersDatabase)
        {
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
            var monsterBehaviour = monster.GetComponent<IMonsterBehaviour>();
            monsterBehaviour.AddWaypoints(waypoints);
            monsterBehaviour.StartMove();
            _monsters.Add(monster);
            return monsterBehaviour;
        }
        
        public void Reset()
        {
            foreach (var monster in _monsters)
            {
                GameObject.Destroy(monster);
            }
            _monsters.Clear();
        }
    }
}