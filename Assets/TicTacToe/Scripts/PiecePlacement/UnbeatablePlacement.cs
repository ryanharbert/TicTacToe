using System.Collections.Generic;
using GridSystem;
using UnityEngine;
using Grid = GridSystem.Grid;

namespace TicTacToe
{
    public class UnbeatablePlacement : ComputerPlacement
    {
        [SerializeField] private bool worstPlayer = false;
        
        PieceType MaxPlayer => currentTurn;
        PieceType MinPlayer => currentTurn == PieceType.X ? PieceType.O : PieceType.X;
        
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
                    tempState.State[position] = MaxPlayer;
                    score = MiniMax(MinPlayer, tempState, -100000, 100000);
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
            if (tempState.GameOver(out OutcomeData data))
            {
                if (data.Winner == PieceType.Empty)
                {
                    return 0;
                }
                else if(data.Winner == MaxPlayer)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }

            int score;
            
            foreach (Vector2Int position in tempState.EmptyPositions())
            {
                tempState.State[position] = current;
                PieceType other = current == PieceType.X ? PieceType.O : PieceType.X;
                score = MiniMax(other, tempState, alpha, beta);
                tempState.State[position] = PieceType.Empty;

                if (current == MaxPlayer)
                {
                    if (score > alpha)
                    {
                        alpha = score;
                    }
                }
                else
                {
                    if (score < beta)
                    {
                        beta = score;
                    }
                }

                if (alpha > beta)
                {
                    break;
                }
            }

            if (current == MaxPlayer)
            {
                return alpha;
            }
            else
            {
                return beta;
            }
        }
    }
}