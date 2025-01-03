using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public enum GameState
    {
        Gameplay,
        Paused,
        GameWin,
        GamerOver
    }

    public class GameManager : MonoBehaviour
    {
        internal static string _sceneToLoad = "Level 1";
        internal static string _currentScene;
        public GameState _currentState;
        private GameState _previousState;
        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _gameOverScreen;
        private bool _isGameOver = false;
        private CameraLevel _cameraLevel;
        private static PlayerPosition _playerPosition;
        private PlayerStats _playerStats;

        private void Awake()
        {
            _currentScene = _sceneToLoad;
            SceneManager.LoadScene(_sceneToLoad, LoadSceneMode.Additive);
            _playerPosition = FindObjectOfType<PlayerPosition>();
        }

        private void Update()
        {
            switch (_currentState)
            {
                case GameState.Gameplay:
                    CheckPause();
                    break;
                case GameState.Paused:
                    CheckPause();
                    break;
                case GameState.GameWin:
                    Time.timeScale = 0f;
                    break;
                case GameState.GamerOver:
                    if (!_isGameOver)
                    {
                        _isGameOver = true;
                        Time.timeScale = 0f;
                    }

                    break;
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _currentScene = scene.name;

            _cameraLevel = FindObjectOfType<CameraLevel>();
            if (_cameraLevel != null)
            {
                _cameraLevel.CameraSize();
            }

            if (_playerPosition != null)
            {
                _playerPosition.ChangePosition();
            }
        }

        private static void LoadScenes(string nextScene)
        {
            if (_playerPosition != null)
            {
                PlayerPosition.LevelPositions = _playerPosition._playerTransform.position;
            }


            if (_currentScene != null)
            {
                SceneManager.UnloadSceneAsync(_currentScene);
            }

            _currentScene = nextScene;
            SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        }

        public void RetryLevel()
        {
            SceneManager.LoadScene(_currentScene);
            _gameOverScreen.SetActive(false);
            ChangeState(GameState.Gameplay);
            Time.timeScale = 1f;
        }

        private void ChangeState(GameState newState)
        {
            _currentState = newState;
        }

        public void ResumeGame()
        {
            if (_currentState != GameState.Paused) return;
            ChangeState(_previousState);
            Time.timeScale = 1f;
            _pauseScreen.SetActive(false);
        }

        private void GamePaused()
        {
            if (_currentState == GameState.Paused) return;
            _previousState = _currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
            _pauseScreen.SetActive(true);
        }

        private void CheckPause()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            if (_currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                GamePaused();
            }
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void GameOver()
        {
            ChangeState(GameState.GamerOver);

            if (_currentState == GameState.GamerOver)
            {
                _gameOverScreen.SetActive(true);
            }
        }

        public void NextLevel()
        {
            BombManager bombManager = FindObjectOfType<BombManager>();
            if (bombManager != null)
            {
                bombManager.DestroyAllBombs();
            }

            LoadScenes(SceneTransition.NextScene);
        }

        public void ReturnToLevel1()
        {
            LoadScenes("Level 1");
            _winScreen.SetActive(false);
        }

        public void ShowWinScreen()
        {
            _winScreen.SetActive(true);
        }
    }
}