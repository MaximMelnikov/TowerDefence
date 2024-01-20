using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Services.MapProceduralGenerator.MapFactory
{
    public class MapFactory : IMapFactory
    {
        private readonly MapTilesDatabase _mapTilesDatabase;
        private readonly Transform _mapContainer;

        public MapFactory(MapTilesDatabase mapTilesDatabase)
        {
            _mapTilesDatabase = mapTilesDatabase;

            _mapContainer = new GameObject("MapContainer").transform;
        }
        
        public void Reset()
        {
            foreach (Transform child in _mapContainer)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        
        public async UniTask<IMapObject> CreateObject(TileType type, Vector2 position, int rotation, float yOffset = 0)
        {
            var gameobject = await _mapTilesDatabase.GetTile(type)
                .InstantiateAsync(
                    position.ToVector3WithYToZ()+ (Vector3.up * yOffset), 
                    Quaternion.Euler(0, rotation, 0),
                    _mapContainer).Task;
            gameobject.name = $"({position.x},{position.y}) {type}";
            return new MapObject(gameobject.transform, position);
        }

        public async UniTask<IMapObject> CreateProp(int index, Vector2 position, int rotation, float yOffset = 0)
        {
            var gameobject = await _mapTilesDatabase.Decorations[index]
                .InstantiateAsync(
                    position.ToVector3WithYToZ() + (Vector3.up * yOffset),
                    Quaternion.Euler(0, rotation, 0),
                    _mapContainer).Task;
            gameobject.name = $"({position.x},{position.y}) Prop {index}";
            return new MapObject(gameobject.transform, position);
        }
    }
}