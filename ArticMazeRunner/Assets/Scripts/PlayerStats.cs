using UnityEngine;

namespace Assets
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;
        public int _currentHealth;
        [SerializeField] private PlayerScriptableObjet _playerData;
        [SerializeField] private Player _player;

        public void Start()
        {
            _maxHealth = _playerData.Health;
            _currentHealth = _maxHealth;
        }

        private void Update()
        {
            UIManager.Instance._healthText.text = "HP: " + _currentHealth.ToString();
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth < 0)
            {
                _currentHealth = 0;
            }

            if (_currentHealth <= 0)
            {
                var gameOver = FindObjectOfType<GameManager>();
                _player._rigidbody2D.velocity = new Vector3(0, 0, 0);
                _player.enabled = false;
                gameOver.GameOver();
            }
        }

        public void Healing(int amount)
        {
            if (_currentHealth <= _maxHealth)
            {
                _currentHealth += amount;

                if (_currentHealth > _maxHealth)
                {
                    _currentHealth = _maxHealth;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bomb"))
            {
                TakeDamage(999);
            }
        }
    }
}