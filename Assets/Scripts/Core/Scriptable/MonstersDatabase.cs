using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Scriptable
{
    [CreateAssetMenu(fileName = "MonstersDatabase", menuName = "Scriptables/MonstersDatabase")]
    public class MonstersDatabase : ScriptableObject
    {
        [SerializeField] private AssetReference _capsule;
        [SerializeField] private AssetReference _sphere;
        [SerializeField] private AssetReference _box;
        
        public AssetReference GetMonster(MonsterType monsterType)
        {
            switch (monsterType)
            {
                case MonsterType.Capsule:
                    return _capsule;
                case MonsterType.Sphere:
                    return _sphere;
                case MonsterType.Box:
                    return _box;
                default:
                    return _capsule;
            }
        }
    }
}