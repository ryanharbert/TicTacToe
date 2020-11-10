using System.Collections.Generic;
using UnityEngine;
using Grid = GridSystem.Grid;

namespace TicTacToe
{
    public class GameState : MonoBehaviour
    {
        private static readonly Dictionary<Vector2Int, PieceType> State = new Dictionary<Vector2Int, PieceType>();

        public void NewGame()
        {
            State.Clear();
            foreach (var kvp in Grid.Instance.Tiles)
            {
                State.Add(kvp.Key, PieceType.Empty);
            }
        }

        public void PiecePlaced(Vector2Int position, PieceType type)
        {
            if (State[position] != PieceType.Empty)
            {
                Debug.LogError("A non empty tile has been set with a different type.");
            }
            
            State[position] = type;
        }

        public bool GameOver(out PieceType winner)
        {
            winner = PieceType.Empty;

            return false;
        }
    }
}