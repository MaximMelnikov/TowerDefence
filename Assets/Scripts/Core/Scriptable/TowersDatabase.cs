using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core.Scriptable
{
    [CreateAssetMenu(fileName = "TowersDatabase", menuName = "Scriptables/TowersDatabase")]
    public class TowersDatabase : ScriptableObject
    {
        [SerializeField] private AssetReference _tower;
        
        public AssetReference GetTower(string id)
        {
            return _tower;
        }
    }
}