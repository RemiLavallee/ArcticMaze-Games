using UnityEngine;

namespace Assets
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] private EnemyScriptableObject _enemyData;
        private int _currentDamage;

        private void Awake()
        {
            _currentDamage = _enemyData.Damage;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                var player = col.gameObject.GetComponent<PlayerStats>();
                player.TakeDamage(_currentDamage);
            }
        }
    }
}