using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GridSystem
{
    public class GridTile : MonoBehaviour
    {
        public Vector2Int Position => position;
        [SerializeField] private Vector2Int position;

        public List<GridTile> Neighbors => neighbors;
        [SerializeField] private List<GridTile> neighbors;

        public List<GridObject> gridObjects = new List<GridObject>();

        public UnityEvent onGridChange;

        public bool IsOccupied => gridObjects.Count > 0;

        /// <summary>
        /// Called when an Grid Tile is created.
        /// </summary>
        public void OnCreate(Vector2Int position)
        {
            this.position = position;
        }

        /// <summary>
        /// Called whenever tiles are changed on the grid.
        /// </summary>
        public void TilesChangedOnGrid(List<GridTile> neighbors)
        {
            this.neighbors = neighbors;
            
            onGridChange.Invoke();
        }

        public bool TryGetNeighborOfType(NeighborType type, out GridTile tile)
        {
            foreach (var neighbor in neighbors)
            {
                if (neighbor.position == position + GridUtility.NeighborPositions[type])
                {
                    tile = neighbor;
                    return true;
                }
            }

            tile = null;
            return false;
        }
    }
}