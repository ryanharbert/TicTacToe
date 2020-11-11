using GridSystem;
using UnityEngine;
using Grid = GridSystem.Grid;

namespace TicTacToe
{
    public class UnbeatablePlacement : ComputerPlacement
    {
        protected override Vector2Int ComputerSelectPiece()
        {
            return MiniMax();
        }

        Vector2Int MiniMax()
        {
            
            // Outcome { Loss = -1, Tied = 0, Win = 1 }
            // Score = Outcome * (SpacesLeft + 1)

            PieceType maxPlayer = currentTurn;
            PieceType minPlayer = maxPlayer == PieceType.X ? PieceType.O : PieceType.X;
            
            // On winner return score back up to top
            
            return Vector2Int.zero;
        }
    }
}