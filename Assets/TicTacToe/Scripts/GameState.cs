using System.Collections.Generic;
using UnityEngine;
using Grid = GridSystem.Grid;

namespace TicTacToe
{
    public struct GameState
    {
        public Dictionary<Vector2Int, PieceType> State;

        public GameType GameType { get; private set; }

        public GameState(List<Vector2Int> positions, GameType gameType)
        {
            State = new Dictionary<Vector2Int, PieceType>();
            foreach (Vector2Int position in positions)
            {
                State.Add(position, PieceType.Empty);
            }

            GameType = gameType;
        }

        public GameState(Dictionary<Vector2Int, PieceType> state, GameType gameType)
        {
            State = new Dictionary<Vector2Int, PieceType>();
            foreach (var kvp in state)
            {
                State.Add(kvp.Key, kvp.Value);
            }

            this.GameType = gameType;
        }

        public void PiecePlaced(Vector2Int position, PieceType type)
        {
            if (State[position] != PieceType.Empty)
            {
                Debug.LogError("A non empty tile has been set with a different type.");
            }
            
            State[position] = type;
        }

        public bool GameOver(out OutcomeData data)
        {
            return GameType.GameOver(State, out data);
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

        public Vector2Int RandomEmptyPosition()
        {
            List<Vector2Int> tilePositions = EmptyPositions();

            if (tilePositions.Count > 0)
            {
                int randomIndex = Random.Range(0, tilePositions.Count);
                return tilePositions[randomIndex];
            }

            Debug.LogError("You tried to get a random empty position when all the positions were filled.");
            return Vector2Int.zero;
        }
        
    }

    public struct OutcomeData
    {
        public PieceType Winner;
        public List<Vector2Int> WinPositions;
        
        public OutcomeData(PieceType winner, List<Vector2Int> winPositions = null)
        {
            Winner = winner;
            WinPositions = winPositions;
        }
    }
}