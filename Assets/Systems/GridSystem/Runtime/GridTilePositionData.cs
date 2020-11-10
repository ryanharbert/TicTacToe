using System;
using UnityEngine;

namespace GridSystem
{
    [Serializable]
    public class GridTilePositionData
    {
        public Vector2Int Position;
        public GridTile Tile;

        public GridTilePositionData(Vector2Int position, GridTile tile)
        {
            Position = position;
            Tile = tile;
        }
    }
}