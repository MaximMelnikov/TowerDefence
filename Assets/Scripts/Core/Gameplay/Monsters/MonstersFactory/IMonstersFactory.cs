using Core.Scriptable;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Gameplay.Monsters.MonstersFactory
{
    public interface IMonstersFactory
    {
        public UniTask<IMonsterBehaviour> CreateMonster(MonsterType type, Vector3 position, Vector2[] waypoints);
        public List<IMonsterBehaviour> GetMonsters();
        public void DestroyMonster(IMonsterBehaviour monster);
        public void Reset();
    }
}