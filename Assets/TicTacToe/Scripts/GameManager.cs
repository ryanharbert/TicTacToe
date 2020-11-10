using System;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;
using Grid = GridSystem.Grid;

namespace TicTacToe
{
    [RequireComponent(typeof(HoverDisplay), typeof(GameState))]
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GridObject xGridObject;
        [SerializeField] private GridObject oGridObject;

        private HoverDisplay hoverDisplay;
        private GameState gameState;

        private PieceSelection xSelection;
        private PieceSelection oSelection;

        protected override void Awake()
        {
            base.Awake();

            hoverDisplay = GetComponent<HoverDisplay>();
            gameState = GetComponent<GameState>();
        }

        public void StartGame(PieceSelection xSelection, PieceSelection oSelection)
        {
            this.xSelection = xSelection;
            this.oSelection = oSelection;
        }
    }
}