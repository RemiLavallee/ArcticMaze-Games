using UnityEngine;

namespace Assets
{
    public class CameraLevel : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public void CameraSize()
        {
            var transformPosition = _camera.transform.position;

            switch (GameManager._currentScene)
            {
                case "Level 1":
                    transformPosition.x = 1f;
                    _camera.orthographicSize = 3f;
                    break;
                case "Level 2":
                    transformPosition.x = 0.75f;
                    transformPosition.y = 1.5f;
                    _camera.orthographicSize = 4.5f;
                    break;
                case "Level 3":
                    transformPosition.x = 1.75f;
                    transformPosition.y = 0.45f;
                    _camera.orthographicSize = 6f;
                    break;
            }

            _camera.transform.position = transformPosition;
        }
    }
}