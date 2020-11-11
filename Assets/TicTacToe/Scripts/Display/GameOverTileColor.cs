using System;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;
using Grid = GridSystem.Grid;

namespace TicTacToe
{
    public class GameOverTileColor : MonoBehaviour
    {
        [SerializeField] private Color winningObjectColor = Color.green;
        
        private void Awake()
        {
            GameManager.GameOver += ColorObjects;
        }

        private void OnDestroy()
        {
            GameManager.GameOver -= ColorObjects;
        }

        void ColorObjects(OutcomeData data)
        {
            if (data.WinPositions == null) return;
            
            foreach (var position in data.WinPositions)
            {
                List<GridObject> gridObjects = Grid.Instance.Tiles[position].gridObjects;
                
                gridObjects[0].GetComponent<ColorChanger>().ChangeColor(winningObjectColor);
            }
        }
    }
}