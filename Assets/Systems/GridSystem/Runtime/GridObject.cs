using UnityEngine;

namespace GridSystem
{
    public class GridObject : MonoBehaviour
    {
        public GridTile Tile { get; private set; }

        public void OnCreate(GridTile tile)
        {
            Tile = tile;
            tile.GridObjects.Add(this);
        }

        public void OnClear()
        {
            Tile.GridObjects.Remove(this);
        }
    }
}