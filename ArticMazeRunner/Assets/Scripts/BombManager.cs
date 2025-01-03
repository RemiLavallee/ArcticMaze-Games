using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets
{
    public class BombManager : MonoBehaviour
    {
        [SerializeField] private Tilemap _floorTilemap;
        [SerializeField] private GameObject _bombPrefab;
        [SerializeField] private LayerMask _occupiedLayer;
        [SerializeField] private float _spawnInterval = 10f;
        private readonly List<GameObject> _bombs = new();

        private void SpawnBomb()
        {
            var randomTilePosition = GetRandomFloorTile();
            if (randomTilePosition == Vector3Int.zero) return;
            var offset = new Vector3(0.25f, 0.24f, 0);
            var worldPosition = _floorTilemap.CellToWorld(randomTilePosition) + offset;
            var bombInstance = Instantiate(_bombPrefab, worldPosition, Quaternion.identity);
            _bombs.Add(bombInstance);
        }

        private void Start()
        {
            StartCoroutine(SpawnBombRoutine());
        }

        private IEnumerator SpawnBombRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnInterval);
                SpawnBomb();
            }
        }

        private Vector3Int GetRandomFloorTile()
        {
            var bounds = _floorTilemap.cellBounds;
            var floorPositions = new List<Vector3Int>();

            for (var x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (var y = bounds.yMin; y < bounds.yMax; y++)
                {
                    var tilePosition = new Vector3Int(x, y, 0);
                    if (_floorTilemap.HasTile(tilePosition) && !IsTileOccupied(tilePosition))
                    {
                        floorPositions.Add(tilePosition);
                    }
                }
            }

            if (floorPositions.Count == 0) return Vector3Int.zero;

            var randomIndex = Random.Range(0, floorPositions.Count);
            return floorPositions[randomIndex];
        }

        private bool IsTileOccupied(Vector3Int tilePosition)
        {
            var worldPosition = _floorTilemap.CellToWorld(tilePosition) + new Vector3(0.1f, 0.1f, 0);
            var hit = Physics2D.OverlapCircle(worldPosition, 0.1f, _occupiedLayer);
            return hit != null;
        }

        public void DestroyAllBombs()
        {
            foreach (GameObject bomb in _bombs)
            {
                Destroy(bomb);
            }

            _bombs.Clear();
        }
    }
}