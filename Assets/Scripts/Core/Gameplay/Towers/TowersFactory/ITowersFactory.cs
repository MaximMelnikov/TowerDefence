using Core.Gameplay;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowersFactory
{
    public UniTask<ITower> CreateTower(string id, Vector3 position);
    public void Reset();
}