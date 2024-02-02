using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Gameplay.Monsters
{
    public interface IMonsterBehaviour
    {
        public void AddWaypoints(Vector2[] waypoints);
        public void StartMove();
    }
}