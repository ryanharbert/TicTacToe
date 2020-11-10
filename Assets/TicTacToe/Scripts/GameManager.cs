using System;
using GridSystem;
using UnityEngine;
using Grid = GridSystem.Grid;

namespace TicTacToe
{
    [RequireComponent(typeof(HoverDisplay))]
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GridObject xGridObject;
        [SerializeField] private GridObject oGridObject;

        protected override void Awake()
        {
            base.Awake();

            TileInput.TileSelected += TileSelected;
        }

        private void OnDestroy()
        {
            TileInput.TileSelected -= TileSelected;
        }

        void TileSelected(GridTile tile)
        {
            if(TileOccupied(tile)) return;

            Grid.Instance.InstantiateGridObject(xGridObject, tile.Position);
        }

        bool TileOccupied(GridTile tile)
        {
            return tile.GridObjects.Count > 0;
        }
    }
}