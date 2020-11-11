using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public class BasicGame : GameType
    {
        public override bool GameOver(Dictionary<Vector2Int, PieceType> state, out OutcomeData data)
        {
            if (CheckColumnOrRowForWinner(true, state, out data))
                return true;

            if (CheckColumnOrRowForWinner(false, state, out data))
                return true;

            if (CheckDiagonalForWinner(state, out data))
                return true;

            data = new OutcomeData(PieceType.Empty, new List<Vector2Int>());
            if (AllTilesFilled(state))
                return true;
            
            return false;
        }

        bool AllTilesFilled(Dictionary<Vector2Int, PieceType> state)
        {
            foreach (var kvp in state)
            {
                if (kvp.Value == PieceType.Empty)
                {
                    return false;
                }
            }
            
            return true;
        }

        bool CheckColumnOrRowForWinner(bool column, Dictionary<Vector2Int, PieceType> state, out OutcomeData data)
        {
            data = new OutcomeData();
            for(int x = 0; x < 3; x++)
            {
                PieceType possibleWinner = PieceType.Empty;
                data.WinPositions = new List<Vector2Int>();
                for (int y = 0; y < 3; y++)
                {
                    PieceType typeAtPos;
                    if (column)
                    {
                        Vector2Int pos = new Vector2Int(x, y);
                        data.WinPositions.Add(pos);
                        typeAtPos = state[pos];
                    }
                    else
                    {
                        Vector2Int pos = new Vector2Int(y, x);
                        data.WinPositions.Add(pos);
                        typeAtPos = state[pos];
                    }
                    
                    if (y == 0)
                    {
                        if (typeAtPos == PieceType.Empty)
                        {
                            break;
                        }

                        possibleWinner = typeAtPos;
                        continue;
                    }

                    if (possibleWinner != typeAtPos)
                    {
                        break;
                    }

                    if (y == 2)
                    {
                        data.Winner = possibleWinner;
                        return true;
                    }
                }
            }

            data = new OutcomeData();
            return false;
        }

        bool CheckDiagonalForWinner(Dictionary<Vector2Int, PieceType> state, out OutcomeData data)
        {
            if (state[new Vector2Int(1,1)] == PieceType.Empty)
            {
                data = new OutcomeData();
                return false;
            }
            else if (state[new Vector2Int(0,0)] == state[new Vector2Int(1,1)] && state[new Vector2Int(1,1)] == state[new Vector2Int(2,2)])
            {
                data = new OutcomeData(state[new Vector2Int(1, 1)], new List<Vector2Int>() { new Vector2Int(0,0), new Vector2Int(1,1), new Vector2Int(2,2) });
                return true;
            }
            else if (state[new Vector2Int(0,2)] == state[new Vector2Int(1,1)] && state[new Vector2Int(1,1)] == state[new Vector2Int(2,0)])
            {
                data = new OutcomeData(state[new Vector2Int(1, 1)], new List<Vector2Int>() {new Vector2Int(0, 2), new Vector2Int(1, 1), new Vector2Int(2, 0)});
                return true;
            }
            
            data = new OutcomeData();
            return false;
        }
    }
}