using Unity.VisualScripting;
using UnityEngine;

namespace Core.Gameplay
{
    public class Camera : MonoBehaviour
    {
        public Rect rect;
        private void Update()
        {
            Vector3 llPoint = UnityEngine.Camera.main.WorldToViewportPoint(rect.min);
            Vector3 urPoint = UnityEngine.Camera.main.WorldToViewportPoint(rect.max);
            var viewportRect = new Rect(llPoint.x, llPoint.y, urPoint.x - llPoint.x, urPoint.y - llPoint.y);
            UnityEngine.Camera.main.transform.position = new Vector3(
                rect.center.x, 
                UnityEngine.Camera.main.transform.position.y, 
                rect.center.y);
            UnityEngine.Camera.main.orthographicSize = (Mathf.Max(rect.width, rect.height) / 2) + .5f;
        }
    }
}