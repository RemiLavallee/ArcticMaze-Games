using TMPro;
using UnityEngine;

namespace Assets
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float _countdown = 5f;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private Canvas _bombCanvas;

        private void Awake()
        {
            var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            if (_bombCanvas != null && mainCamera != null)
            {
                _bombCanvas.worldCamera = mainCamera;
            }
        }

        private void Start()
        {
            _timerText = GetComponentInChildren<TMP_Text>();
        }

        private void Update()
        {
            _countdown -= Time.deltaTime;
            UpdateTimerDisplay();

            if (_countdown <= 0f)
            {
                Explode();
            }
        }

        private void UpdateTimerDisplay() => _timerText.text = Mathf.CeilToInt(_countdown).ToString();

        private void Explode()
        {
            Destroy(gameObject);
        }
    }
}