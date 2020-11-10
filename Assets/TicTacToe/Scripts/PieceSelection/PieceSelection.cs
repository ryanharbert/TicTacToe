using GridSystem;
using UnityEngine;

namespace TicTacToe
{
    public abstract class PieceSelection
    {
        protected GridObject gridObject;
        
        public void StartSelection(GridObject gridObject)
        {
            this.gridObject = gridObject;
            
            OnSelection();
        }

        protected abstract void OnSelection();
        
        void PieceSelected(GridTile tile)
        {
            if(tile.IsOccupied) return;

            GridSystem.Grid.Instance.InstantiateGridObject(gridObject, tile.Position);
        }
        
        // Start
        
        // Piece Selected
    }
}