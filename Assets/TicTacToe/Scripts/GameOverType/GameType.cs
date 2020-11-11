using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public abstract class GameType
    {
        public abstract bool GameOver(Dictionary<Vector2Int, PieceType> state, out PieceType winner);
    }
}