using UnityEngine;

namespace Assets
{
    public class Teleporter : MonoBehaviour
    {
        [SerializeField] private Transform _destination;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public Transform GetDestination()
        {
            return _destination;
        }

        public void TeleportAnimation(bool state)
        {
            _animator.SetBool("isTeleported", state);
        }
    }
}