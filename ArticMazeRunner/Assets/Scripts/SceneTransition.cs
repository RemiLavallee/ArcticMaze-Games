using UnityEngine;

namespace Assets
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] private string _nextScene;
        public static string NextScene { get; private set; }

        private void Awake()
        {
            NextScene = _nextScene;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                var gameManager = FindObjectOfType<GameManager>();
                if (gameManager != null)
                {
                    gameManager.NextLevel();
                }
            }
        }
    }
}