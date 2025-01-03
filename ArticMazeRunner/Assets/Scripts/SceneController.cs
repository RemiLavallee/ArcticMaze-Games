using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public class SceneController : MonoBehaviour
    {
        private void Awake()
        {
            if (GameObject.FindGameObjectWithTag("GameController") == null)
            {
                GameManager._sceneToLoad = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene("MainScene");
            }
        }
    }
}