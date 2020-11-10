using UnityEngine;

namespace GridSystem
{
    [RequireComponent(typeof(Grid))]
    public class GridGenerator : MonoBehaviour
    {
        public Vector2 cellSize;
        public Vector2 cellGap;
        public Vector2Int mapSize;
        public GridTile tilePrefab;
    }
}