using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public static class GridUtility
    {
        
        public static List<GridTile> GetNeighbors(Grid grid, GridTile tile)
        {
            List<GridTile> neighbors = new List<GridTile>();
            foreach (var neighborPos in NeighborPositions)
            {
                GridTile maybeNeighbor;
                Vector2Int key = tile.Position + neighborPos.Value;
                if (grid.Tiles.TryGetValue(key, out maybeNeighbor))
                {
                    neighbors.Add(maybeNeighbor);
                }
            }

            return neighbors;
        }
        
        public readonly static Dictionary<NeighborType, Vector2Int> NeighborPositions = new Dictionary<NeighborType, Vector2Int>()
        {
            {NeighborType.Top, new Vector2Int(0, 1)},
            {NeighborType.Bottom, new Vector2Int(0, -1)},
            {NeighborType.Right, new Vector2Int(1, 0)},
            {NeighborType.Left, new Vector2Int(-1, 0)}
        };
    }
    
    public enum NeighborType
    {
        Top,
        Bottom,
        Right,
        Left
    }
}