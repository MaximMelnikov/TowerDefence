using Core.Scriptable;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class TowersFactory : ITowersFactory
{
    private readonly TowersDatabase _towersDatabase;
    private List<GameObject> _towers = new List<GameObject>();

    public TowersFactory(TowersDatabase towersDatabase)
    {
        _towersDatabase = towersDatabase;
    }

    public async UniTask<ITower> CreateTower(string id, Vector3 position)
    {
        var monsterAsset = _towersDatabase.GetTower(id);
        if (monsterAsset == null)
        {
            Debug.LogError($"TowerAsset {id} not found");
            return null;
        }

        var towerGameObject = await monsterAsset.InstantiateAsync(position, Quaternion.identity).Task;
        var tower = towerGameObject.GetComponent<ITower>();
        _towers.Add(towerGameObject);
        return tower;
    }

    public void Reset()
    {
        foreach (var monster in _towers)
        {
            GameObject.Destroy(monster);
        }
        _towers.Clear();
    }
}
