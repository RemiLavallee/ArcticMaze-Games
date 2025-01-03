using UnityEngine;

namespace Assets
{
    public class PlayerTeleport : MonoBehaviour
    {
        private GameObject _currentTeleport;
        [SerializeField] private Transform _movePoint;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (_currentTeleport != null)
                {
                    var destination = _currentTeleport.GetComponent<Teleporter>().GetDestination().position;
                    transform.position = destination;

                    if (_movePoint != null)
                    {
                        _movePoint.position = destination;
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Teleport"))
            {
                _currentTeleport = collision.gameObject;
                _currentTeleport.GetComponent<Teleporter>().TeleportAnimation(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Teleport"))
            {
                if (collision.gameObject == _currentTeleport)
                {
                    _currentTeleport.GetComponent<Teleporter>().TeleportAnimation(false);
                    _currentTeleport = null;
                }
            }
        }
    }
}