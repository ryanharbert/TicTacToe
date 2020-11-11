using System;
using GridSystem;
using UnityEngine;

namespace TicTacToe
{
    public class HoverDisplay : MonoBehaviour
    {
        [SerializeField] private Vector3 hoverDisplayOffset = Vector3.zero;
        [SerializeField] private Color hoverObjectColor = Color.yellow;
        
        [SerializeField] private GameObject xHoverObject;
        [SerializeField] private GameObject oHoverObject;

        private GameObject activeHoverDisplay;
        private bool hoverActive = false;

        private void Awake()
        {
            xHoverObject = HoverObjectSetup(xHoverObject);
            oHoverObject = HoverObjectSetup(oHoverObject);
        }

        GameObject HoverObjectSetup(GameObject g)
        {
            g = Instantiate(g);
            g.GetComponent<ColorChanger>()?.ChangeColor(hoverObjectColor);
            g.SetActive(false);
            return g;
        }

        public void TurnOn(PieceType type)
        {
            TileInput.HoverEnter += HoverEnter;
            TileInput.HoverExit += HoverExit;
            TileInput.TileSelected += HoverExit;

            if (type == PieceType.O)
            {
                activeHoverDisplay = oHoverObject;
            }
            else if(type == PieceType.X)
            {
                activeHoverDisplay = xHoverObject;
            }
        }

        public void TurnOff()
        {
            TileInput.HoverEnter -= HoverEnter;
            TileInput.HoverExit -= HoverExit;
            TileInput.TileSelected -= HoverExit;

            if (hoverActive)
            {
                HoverExit(null);
            }
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