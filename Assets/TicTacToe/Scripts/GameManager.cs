using System;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;
using Grid = GridSystem.Grid;
using Random = UnityEngine.Random;

namespace TicTacToe
{
    [RequireComponent(typeof(GameState))]
    public class GameManager : Singleton<GameManager>
    {
        public static event Action GameRefresh;
        
        [SerializeField] private GridObject xGridObject;
        [SerializeField] private GridObject oGridObject;

        private GameState gameState;

        private PiecePlacement xPlacement;
        private PiecePlacement oPlacement;

        private PiecePlacement currentPlacement;

        private PieceType currentTurn;

        protected override void Awake()
        {
            base.Awake();
            
            gameState = GetComponent<GameState>();
        }

        public void StartGame(PiecePlacement xPlacement, PiecePlacement oPlacement)
        {
            this.xPlacement = xPlacement;
            this.oPlacement = oPlacement;
            
            CleanupGame();

            currentTurn = (PieceType)Random.Range(0, 2);
            StartNextTurn();
        }

        public void CleanupGame()
        {
            gameState.NewGame();
            Grid.Instance.ClearGridObjects();
            if (currentPlacement != null)
            {
                currentPlacement.Cleanup();
            }
        }

        public void PiecePlaced(GridObject gridObject, GridTile tile)
        {
            Grid.Instance.InstantiateGridObject(gridObject, tile.Position);

            gameState.PiecePlaced(tile.Position, currentTurn);

            if(gameState.GameOver(out PieceType winner))
            {
                GameOver(winner);
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
                xPlacement.StartSelection(currentTurn, xGridObject);
            }
            else if(currentTurn == PieceType.X)
            {
                currentTurn = PieceType.O;
                currentPlacement = oPlacement;
                oPlacement.StartSelection(currentTurn, oGridObject);
            }
            else
            {
                Debug.LogError("Don't change the turn to Empty, that doesn't make sense.");
            }
        }

        void GameOver(PieceType winner)
        {
            if (winner == PieceType.O)
            {
                print("O Wins");
            }
            else if(winner == PieceType.X)
            {
                print("X Wins");
            }
            else
            {
                print("Draw");
            }
        }
    }
}