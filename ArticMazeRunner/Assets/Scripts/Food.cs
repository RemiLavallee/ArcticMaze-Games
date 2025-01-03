using UnityEngine;

namespace Assets
{
    public class Food : MonoBehaviour, ICollectible
    {
        [SerializeField] private int _heal;

        public void Collect()
        {
            var player = FindObjectOfType<PlayerStats>();
            player.Healing(_heal);
            Destroy(gameObject);
        }
    }
}