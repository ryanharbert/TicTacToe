using GridSystem;
using UnityEngine;

namespace TicTacToe
{
    [RequireComponent(typeof(HoverDisplay))]
    public class PlayerPlacement : PiecePlacement
    {
        private HoverDisplay hoverDisplay;
        
        private void Awake()
        {
            hoverDisplay = GetComponent<HoverDisplay>();
        }
        
        protected override void OnStartPlacement()
        {
            TileInput.TileSelected += TileSelected;
            hoverDisplay.TurnOn(currentTurn);
        }

        public override void Cleanup()
        {
            TileInput.TileSelected -= TileSelected;
            hoverDisplay.TurnOff();
        }

        void TileSelected(GridTile tile)
        {
            if(tile.IsOccupied) return;
            
            Cleanup();
            
            PlacePiece(tile);
        }
    }
}