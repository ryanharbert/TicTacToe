using System.Collections.Generic;
using GridSystem;
using UnityEngine;
using Grid = GridSystem.Grid;

namespace TicTacToe
{
    public class UnbeatablePlacement : ComputerPlacement
    {
        protected override Vector2Int ComputerSelectPiece()
        {
            // Random if there is no positions filled, otherwise smart
            if (state.EmptyPositions().Count == state.State.Count)
            {
                return state.RandomEmptyPosition();
            }
            else
            {
                GameState tempState = new GameState(state.State, state.GameType);
                List<Vector2Int> possibleMoves = tempState.EmptyPositions();
                Vector2Int bestMove = possibleMoves[0];
                int bestScore = -100000;
                int score;
                
                foreach (Vector2Int position in possibleMoves)
                {
                    tempState.State[position] = currentTurn;
                    PieceType minPlayer = currentTurn == PieceType.X ? PieceType.O : PieceType.X;
                    score = MiniMax(minPlayer, tempState, -100000, 100000);
                    tempState.State[position] = PieceType.Empty;

                    if (bestScore < score)
                    {
                        bestScore = score;
                        bestMove = position;
                    }
                }

                return bestMove;
            }
        }

        int MiniMax(PieceType current, GameState tempState, int alpha, int beta, int depth = -1)
        {
            
                
                

            // Outcome { Loss = -1, Tied = 0, Win = 1 }
            // Score = Outcome * (SpacesLeft + 1)

            PieceType maxPlayer = currentTurn;
            PieceType minPlayer = maxPlayer == PieceType.X ? PieceType.O : PieceType.X;
            
            // On winner return score back up to top
            if (tempState.GameOver(out OutcomeData data))
            {
                if (data.Winner == PieceType.Empty)
                {
                    return 0;
                }
                else if(data.Winner == maxPlayer)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }

            return 1;
        }
    }
}