using UnityEngine;

namespace Core
{
    public static class Extensions
    {
        public static Vector3 ToVector3WithYToZ(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0, vector2.y);
        }
    }
}