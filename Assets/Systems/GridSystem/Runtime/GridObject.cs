using UnityEngine;

namespace GridSystem
{
    public class GridObject : MonoBehaviour
    {
        public GridTile Tile { get; private set; }

        public void OnCreate(GridTile tile)
        {
            Tile = tile;
            tile.gridObjects.Add(this);
        }

        public void OnClear()
        {
            Tile.gridObjects.Remove(this);
        }
    }
}