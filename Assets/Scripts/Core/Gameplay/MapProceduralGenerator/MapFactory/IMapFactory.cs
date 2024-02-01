using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Gameplay.MapProceduralGenerator.MapFactory
{
    public interface IMapFactory
    {
        public UniTask<IMapObject> CreateObject(TileType type, Vector2 position, int rotation, float yOffset = 0);
        public UniTask<IMapObject> CreateProp(int index, Vector2 position, int rotation, float yOffset = 0);

        public void Reset();
    }
}