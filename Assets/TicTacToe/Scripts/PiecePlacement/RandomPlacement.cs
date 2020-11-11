using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TicTacToe
{
    public class RandomPlacement : ComputerPlacement
    {
        protected override Vector2Int ComputerSelectPiece()
        {
            return state.RandomEmptyPosition();
        }
    }
}