using GridSystem;
using UnityEngine;
using Grid = GridSystem.Grid;

namespace TicTacToe
{
    public class HoverDisplay : MonoBehaviour
    {
        [SerializeField] private Vector3 hoverDisplayOffset = Vector3.zero;
        
        [SerializeField] private GameObject xHoverObject;
        [SerializeField] private GameObject oHoverObject;

        [SerializeField] private GameObject activeHoverDisplay;

        private bool hoverActive = false;

        private void Awake()
        {
            xHoverObject = Instantiate(xHoverObject);
            // MeshRenderer[] renderers = xHoverObject.GetComponentsInChildren<MeshRenderer>();
            // foreach (var r in renderers)
            // {
            //     r.material.color = Color.yellow;
            // }
            xHoverObject.SetActive(false);
            
            oHoverObject = Instantiate(oHoverObject);
            oHoverObject.SetActive(false);

            TileInput.HoverEnter += HoverEnter;
            TileInput.HoverExit += HoverExit;
            TileInput.TileSelected += HoverExit;

            activeHoverDisplay = oHoverObject;

            // On turn change disable current and set new active display
        }

        private void OnDestroy()
        {
            TileInput.HoverEnter -= HoverEnter;
            TileInput.HoverExit -= HoverExit;
            TileInput.TileSelected -= HoverExit;
        }

        void HoverEnter(GridTile tile)
        {
            if (tile.IsOccupied) return;
            
            hoverActive = true;
            activeHoverDisplay.SetActive(true);
            
            MoveDisplayToGridPosition(tile);
        }

        void HoverExit(GridTile tile)
        {
            if (!hoverActive) return;

            hoverActive = false;
            activeHoverDisplay.SetActive(false);
        }

        void MoveDisplayToGridPosition(GridTile tile)
        {
            activeHoverDisplay.transform.SetParent(tile.transform);
            activeHoverDisplay.transform.localPosition = hoverDisplayOffset;
        }
    }
}