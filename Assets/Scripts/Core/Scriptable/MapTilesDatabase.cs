using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "MapTilesDatabase", menuName = "Scriptables/MapTilesDatabase")]
public class MapTilesDatabase : ScriptableObject
{
    [SerializeField] private List<AssetReference> _decorations;
    
    [SerializeField] private AssetReference _straight;
    [SerializeField] private AssetReference _turn;
    [SerializeField] private AssetReference _split;
    [SerializeField] private AssetReference _crossroad;
    [SerializeField] private AssetReference _spawnpoint;
    [SerializeField] private AssetReference _castle;
    [SerializeField] private AssetReference _empty;
    [SerializeField] private AssetReference _grass;
    [SerializeField] private AssetReference _towerSlot;
    [SerializeField] private AssetReference _tower1;
    [SerializeField] private AssetReference _tower2;
    
    public List<AssetReference> Decorations => _decorations;
    
    public AssetReference GetTile(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Straight:
                return _straight;
            case TileType.Turn:
                return _turn;
            case TileType.Split:
                return _split;
            case TileType.Crossroad:
                return _crossroad;
            case TileType.Spawnpoint:
                return _spawnpoint;
            case TileType.Castle:
                return _castle;
            case TileType.Grass:
                return _grass;
            case TileType.TowerSlot:
                return _towerSlot;
            default:
                return _empty;
        }
    }
}