using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class BasicGame : GameType
    {
        public override bool GameOver(Dictionary<Vector2Int, PieceType> state, out PieceType winner)
        {
            bool allTilesFilled = true;
            foreach (var kvp in state)
            {
                if (kvp.Value == PieceType.Empty)
                {
                    allTilesFilled = false;
                }
            }

            if (allTilesFilled)
            {
                winner = PieceType.Empty;
                return true;
            }
            
            winner = PieceType.Empty;
            return false;
        }
    }
}