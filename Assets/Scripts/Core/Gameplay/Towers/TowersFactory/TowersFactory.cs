using Core.Scriptable;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TowersFactory : ITowersFactory
{
    private readonly TowersDatabase _towersDatabase;
    private List<GameObject> _towers = new List<GameObject>();
    private DiContainer _container;

    public TowersFactory(DiContainer container, TowersDatabase towersDatabase)
    {
        _container = container;
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
        _container.InjectGameObject(towerGameObject);
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
