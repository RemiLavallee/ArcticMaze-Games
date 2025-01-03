using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/Player")]
    public class PlayerScriptableObjet : ScriptableObject
    {
        [SerializeField] private int _health;

        public int Health
        {
            get => _health;
            private set => _health = value;
        }
    }
}