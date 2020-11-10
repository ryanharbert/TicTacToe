using GridSystem;
using UnityEngine;
using Grid = UnityEngine.Grid;

namespace TicTacToe
{
    public class PlayerPieceSelection : PieceSelection
    {
        private GridObject temp;
        
        protected void Awake()
        {
            TileInput.TileSelected += TileSelected;
        }

        private void OnDestroy()
        {
            TileInput.TileSelected -= TileSelected;
        }

        void TileSelected(GridTile tile)
        {
            if(tile.IsOccupied) return;

            GridSystem.Grid.Instance.InstantiateGridObject(temp, tile.Position);
        }
        
        // Start
        
        // Piece Selected
    }
}