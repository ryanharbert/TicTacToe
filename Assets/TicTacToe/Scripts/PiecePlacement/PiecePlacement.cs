using System;
using GridSystem;
using UnityEngine;
using Grid = GridSystem.Grid;

namespace TicTacToe
{
    public abstract class PiecePlacement : MonoBehaviour
    {
        protected GridObject gridObject;
        protected PieceType currentTurn;
        
        public void StartSelection(PieceType currentTurn, GridObject gridObject)
        {
            this.currentTurn = currentTurn;
            this.gridObject = gridObject;
            
            OnStartPlacement();
        }

        protected abstract void OnStartPlacement();

        public virtual void Cleanup(){}

        protected void PlacePiece(Vector2Int position)
        {
            if (Grid.Instance.Tiles.TryGetValue(position, out GridTile tile))
            {
                PlacePiece(tile);
            }
            else
            {
                Debug.LogError("Could not find the tile on the grid when selecting the piece.");
            }
        }

        protected void PlacePiece(GridTile tile)
        {
            if (tile.IsOccupied)
            {
                Debug.LogError("The tile selected was already occupied by another piece.");
                return;
            }
            
            GameManager.Instance.PiecePlaced(gridObject, tile);
        }
    }
}