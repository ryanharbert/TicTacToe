using System;
using System.Collections.Generic;
using System.Linq;
using GridSystem;
using UnityEngine;
using Grid = GridSystem.Grid;
using Random = UnityEngine.Random;

namespace TicTacToe
{
    public class GameManager : Singleton<GameManager>
    {
        public static event Action<OutcomeData> GameOver;
        
        [SerializeField] private GridObject xGridObject;
        [SerializeField] private GridObject oGridObject;

        private GameState gameState;

        private PiecePlacement xPlacement;
        private PiecePlacement oPlacement;
        private GameType gameType;

        private PiecePlacement currentPlacement;

        private PieceType currentTurn;

        public void StartGame(PiecePlacement xPlacement, PiecePlacement oPlacement, GameType gameType)
        {
            this.xPlacement = xPlacement;
            this.oPlacement = oPlacement;
            this.gameType = gameType;
            
            CleanupGame();

            currentTurn = (PieceType)Random.Range(0, 2);
            StartNextTurn();
        }

        void CleanupGame()
        {
            Grid.Instance.ClearGridObjects();
            if (currentPlacement != null)
            {
                currentPlacement.Cleanup();
            }
            
            gameState = new GameState(Grid.Instance.Tiles.Keys.ToList(), gameType);
        }

        public void PiecePlaced(GridTile tile)
        {
            if (currentTurn == PieceType.X)
            {
                Grid.Instance.InstantiateGridObject(xGridObject, tile.Position);
            }
            else
            {
                Grid.Instance.InstantiateGridObject(oGridObject, tile.Position);
            }

            gameState.PiecePlaced(tile.Position, currentTurn);

            if(gameState.GameOver(out OutcomeData data))
            {
                if (data.Winner == PieceType.O)
                {
                    print("O Wins");
                }
                else if(data.Winner == PieceType.X)
                {
                    print("X Wins");
                }
                else
                {
                    print("Draw");
                }
                
                GameOver?.Invoke(data);
            }
            else
            {
                StartNextTurn();
            }
        }

        void StartNextTurn()
        {
            if (currentTurn == PieceType.O)
            {
                currentTurn = PieceType.X;
                currentPlacement = xPlacement;
                xPlacement.StartSelection(gameState, currentTurn);
            }
            else if(currentTurn == PieceType.X)
            {
                currentTurn = PieceType.O;
                currentPlacement = oPlacement;
                oPlacement.StartSelection(gameState, currentTurn);
            }
            else
            {
                Debug.LogError("Don't change the turn to Empty, that doesn't make sense.");
            }
        }
        
        
        /// <summary>
        /// Just here to easily tinker with think time.
        /// </summary>
        [SerializeField] private float computerThinkTime = 0.5f;
        private void OnValidate()
        {
            ComputerPlacement.ThinkTime = computerThinkTime;
        }
    }
}