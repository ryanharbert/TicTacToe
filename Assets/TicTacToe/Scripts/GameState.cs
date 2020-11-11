using System.Collections.Generic;
using UnityEngine;
using Grid = GridSystem.Grid;

namespace TicTacToe
{
    public struct GameState
    {
        public Dictionary<Vector2Int, PieceType> State;

        private GameType gameType;
        
        public GameState(List<Vector2Int> positions, GameType gameType)
        {
            State = new Dictionary<Vector2Int, PieceType>();
            foreach (Vector2Int position in positions)
            {
                State.Add(position, PieceType.Empty);
            }

            this.gameType = gameType;
        }

        public GameState(Dictionary<Vector2Int, PieceType> state, GameType gameType)
        {
            State = new Dictionary<Vector2Int, PieceType>();
            foreach (var kvp in state)
            {
                State.Add(kvp.Key, kvp.Value);
            }

            this.gameType = gameType;
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
            return gameType.GameOver(State, out winner);
        }

        public int NumberEmptySquares()
        {
            return EmptyPositions().Count;
        }

        public List<Vector2Int> EmptyPositions()
        {
            List<Vector2Int> tilePositions = new List<Vector2Int>();
            foreach (var kvp in State)
            {
                if (kvp.Value == PieceType.Empty)
                {
                    tilePositions.Add(kvp.Key);
                }
            }

            return tilePositions;
        }
        
    }
}