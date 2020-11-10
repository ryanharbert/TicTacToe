using System;
using GridSystem;
using InteractionSystem;
using UnityEngine;

namespace TicTacToe
{
    [RequireComponent(typeof(Interactable), typeof(GridTile))]
    public class TileInput : MonoBehaviour
    {
        public static Action<GridTile> HoverEnter;
        public static Action<GridTile> HoverExit;
        public static Action<GridTile> TileSelected;

        private Interactable _interactable;
        private GridTile _gridTile;
        
        private void Awake()
        {
            _interactable = GetComponent<Interactable>();
            _gridTile = GetComponent<GridTile>();
            
            _interactable.hoverEnter.AddListener(() => HoverEnter?.Invoke(_gridTile));
            _interactable.hoverExit.AddListener(() => HoverExit?.Invoke(_gridTile));
            _interactable.onRelease.AddListener(() => TileSelected?.Invoke(_gridTile));
        }
    }
}