using UnityEngine;

namespace Assets
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private Transform _movePoint;
        [SerializeField] private LayerMask _whatStopMovement;
        [HideInInspector] public Rigidbody2D _rigidbody2D;
        private PlayerStats _playerStats;

        public bool CanMove { get; set; } = true;

        private void Start()
        {
            _movePoint.parent = null;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _playerStats = FindObjectOfType<PlayerStats>();
        }

        private void FixedUpdate()
        {
            if (!CanMove)
                return;
            if (Vector3.Distance(_rigidbody2D.position, _movePoint.position) > 0.1f)
            {
                Vector2 moveDirection = (_movePoint.position - transform.position).normalized;
                _rigidbody2D.MovePosition(_rigidbody2D.position + moveDirection * (_moveSpeed * Time.fixedDeltaTime));
            }
        }

        private void Update()
        {
            if (!CanMove)
                return;
            transform.position =
                Vector3.MoveTowards(transform.position, _movePoint.position, _moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _movePoint.position) <= 0.1f)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    TryMove(new Vector3(0.5f, 0f, 0f));
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    TryMove(new Vector3(-0.5f, 0f, 0f));
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    TryMove(new Vector3(0f, 0.5f, 0f));
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    TryMove(new Vector3(0f, -0.5f, 0f));
                }
            }
        }

        private void TryMove(Vector3 direction)
        {
            if (!Physics2D.OverlapCircle(_movePoint.position + direction, .05f, _whatStopMovement))
            {
                _movePoint.position += direction;

                _playerStats.TakeDamage(2);
            }
        }
    }
}
