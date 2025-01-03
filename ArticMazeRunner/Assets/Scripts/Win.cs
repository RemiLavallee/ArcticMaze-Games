using UnityEngine;

namespace Assets
{
    public class Win : MonoBehaviour
    {
        private GameManager _gameManager;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                var gameManager = FindObjectOfType<GameManager>();
                if (gameManager != null)
                {
                    gameManager.ShowWinScreen();
                }
            }
        }
    }
}
