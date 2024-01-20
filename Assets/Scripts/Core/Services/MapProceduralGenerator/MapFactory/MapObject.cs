using UnityEngine;

namespace Core.Services.MapProceduralGenerator.MapFactory
{
    public class MapObject : IMapObject
    {
        public Transform Transform { get; }
        public Vector2 Position { get; }
        
        public MapObject(Transform transform, Vector2 position)
        {
            Transform = transform;
            Position = position;
        }
    }
}