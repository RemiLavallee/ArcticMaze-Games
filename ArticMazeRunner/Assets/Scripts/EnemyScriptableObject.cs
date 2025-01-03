using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/enemy")]
    public class EnemyScriptableObject : ScriptableObject
    {
        [SerializeField] private int _damage;
        public int Damage { get => _damage; private set => _damage = value; }
    }
}