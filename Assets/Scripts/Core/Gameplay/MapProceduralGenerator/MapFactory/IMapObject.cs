using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Core.Gameplay.MapProceduralGenerator.MapFactory
{
    public interface IMapObject
    {
        public Transform Transform { get; }
        public Vector2 Position { get; }
        
        
    }
}