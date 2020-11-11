using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TicTacToe
{
    public class RandomPlacement : ComputerPlacement
    {
        protected override Vector2Int ComputerSelectPiece()
        {
            List<Vector2Int> tilePositions = state.EmptyPositions();
            
            int randomIndex = Random.Range(0, tilePositions.Count);
            return tilePositions[randomIndex];
        }
    }
}