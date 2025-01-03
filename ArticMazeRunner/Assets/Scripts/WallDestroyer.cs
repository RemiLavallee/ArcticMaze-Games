using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets
{
    public class WallDestroyer : MonoBehaviour
    {
        [SerializeField] private Tilemap _wallsTilemap;
        [SerializeField] private Tilemap _floorTilemap;
        [SerializeField] private Tile _floorTile;
        [SerializeField] private Color _highlightColor = Color.yellow;
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private int _maxWallHits = 3;
        private int _wallHitsRemaining;
        private Vector3Int _playerPosition;
        private Vector3Int _selectedWallPosition;
        private bool _isSelecting = false;
        private Player _playerScript;

        private void Start()
        {
            _wallHitsRemaining = _maxWallHits;
            _playerScript = FindObjectOfType<Player>();
        }

        private void Update()
        {
            UIManager.Instance._wallHitText.text = _wallHitsRemaining.ToString();
            _playerPosition = _wallsTilemap.WorldToCell(transform.position);
            if (_isSelecting && Input.GetKeyDown(KeyCode.Z))
            {
                CancelSelection();
                _playerScript.CanMove = true;
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) && _playerScript != null)
            {
                _playerScript.CanMove = !_playerScript.CanMove;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!_isSelecting)
                {
                    StartSelectingWall();
                }
                else
                {
                    ReplaceWallWithFloor(_selectedWallPosition);
                    _isSelecting = false;
                    RemoveHighlight(_selectedWallPosition);
                }
            }

            if (_isSelecting)
            {
                TargetWall();
            }
        }

        private void CancelSelection()
        {
            RemoveHighlight(_selectedWallPosition);
            _isSelecting = false;
        }

        private void StartSelectingWall()
        {
            _isSelecting = true;
            _selectedWallPosition = FindNearestWall();

            if (_selectedWallPosition != _playerPosition)
            {
                HighlightWall(_selectedWallPosition);
            }
            else
            {
                _isSelecting = false;
            }
        }

        private Vector3Int FindNearestWall()
        {
            Vector3Int[] directions = { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right };

            foreach (var dir in directions)
            {
                var checkPos = _playerPosition + dir;
                if (_wallsTilemap.HasTile(checkPos))
                {
                    return checkPos;
                }
            }

            return _playerPosition;
        }

        private void ReplaceWallWithFloor(Vector3Int wallPosition)
        {
            if (_wallHitsRemaining > 0 && _wallsTilemap.HasTile(wallPosition))
            {
                _wallsTilemap.SetTile(wallPosition, null);
                _floorTilemap.SetTile(wallPosition, _floorTile);
                _wallHitsRemaining--;
            }

            RemoveHighlight(wallPosition);
            _isSelecting = false;
        }

        private void ChangeSelection(Vector3Int newWallPosition)
        {
            RemoveHighlight(_selectedWallPosition);
            _selectedWallPosition = newWallPosition;
            HighlightWall(_selectedWallPosition);
        }

        private void HighlightWall(Vector3Int wallPosition)
        {
            _wallsTilemap.SetTileFlags(wallPosition, TileFlags.None);
            _wallsTilemap.SetColor(wallPosition, _highlightColor);
        }

        private void RemoveHighlight(Vector3Int wallPosition)
        {
            _wallsTilemap.SetColor(wallPosition, _defaultColor);
        }

        private void TargetWall()
        {
            var newWallPosition = _selectedWallPosition;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                newWallPosition = _playerPosition + Vector3Int.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                newWallPosition = _playerPosition + Vector3Int.down;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                newWallPosition = _playerPosition + Vector3Int.left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                newWallPosition = _playerPosition + Vector3Int.right;
            }

            if (newWallPosition != _selectedWallPosition && _wallsTilemap.HasTile(newWallPosition))
            {
                ChangeSelection(newWallPosition);
            }
        }
    }
}