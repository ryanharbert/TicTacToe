using System;
using GridSystem;
using InteractionSystem;
using UnityEngine;

namespace TicTacToe
{
    [RequireComponent(typeof(Interactable), typeof(GridTile))]
    public class TileInput : MonoBehaviour
    {
        public static event Action<GridTile> HoverEnter;
        public static event Action<GridTile> HoverExit;
        public static event Action<GridTile> TileSelected;

        private Interactable interactable;
        private GridTile gridTile;
        
        private void Awake()
        {
            interactable = GetComponent<Interactable>();
            gridTile = GetComponent<GridTile>();
            
            interactable.hoverEnter.AddListener(() => HoverEnter?.Invoke(gridTile));
            interactable.hoverExit.AddListener(() => HoverExit?.Invoke(gridTile));
            interactable.onRelease.AddListener(() => TileSelected?.Invoke(gridTile));
        }
    }
}