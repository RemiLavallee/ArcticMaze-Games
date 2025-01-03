using UnityEngine;

namespace Assets
{
    public class PlayerPosition : MonoBehaviour
    {
        public Transform _playerTransform;
        public static Vector3 LevelPositions { get; set; }

        private void Start()
        {
            ChangePosition();
        }

        public void ChangePosition()
        {
            Debug.Log("Change position");
            if (LevelPositions != Vector3.zero)
            {
                _playerTransform.position = LevelPositions;
            }
            else
            {
                switch (GameManager._currentScene)
                {
                    case "Level 1":
                        LevelPositions = new Vector3(-1.7514f, -0.2144f, 0);
                        break;
                    case "Level 2":
                        LevelPositions = new Vector3(1.248f, 3.756f, 0);
                        break;
                    case "Level 3":
                        LevelPositions = new Vector3(-4.747f, -0.238f, 0);
                        break;
                }

                _playerTransform.position = LevelPositions;
            }
        }
    }
}