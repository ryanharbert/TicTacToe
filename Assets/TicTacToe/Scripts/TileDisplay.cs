using System;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;

namespace TicTacToe
{
    [RequireComponent(typeof(GridSystem.GridTile))]
    public class TileDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject top;
        [SerializeField] private GameObject bottom;
        [SerializeField] private GameObject left;
        [SerializeField] private GameObject right;

        public void UpdateDisplayOnGridChange()
        {
            GridSystem.GridTile g = GetComponent<GridSystem.GridTile>();
            
            GridSystem.GridTile tempNeighbor;
            top.SetActive(g.TryGetNeighborOfType(NeighborType.Top, out tempNeighbor));
            bottom.SetActive(g.TryGetNeighborOfType(NeighborType.Bottom, out tempNeighbor));
            left.SetActive(g.TryGetNeighborOfType(NeighborType.Left, out tempNeighbor));
            right.SetActive(g.TryGetNeighborOfType(NeighborType.Right, out tempNeighbor));
        }
    }
}