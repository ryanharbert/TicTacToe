using System;
using UnityEngine;

namespace TicTacToe
{
    [RequireComponent(typeof(PlayerPiecePlacement))]
    public class GameSelection : MonoBehaviour
    {
        private void Start()
        {
            PlayerPiecePlacement placement = GetComponent<PlayerPiecePlacement>();
            GameManager.Instance.StartGame(placement, placement);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Start();
            }
        }

        public void TeamSelection()
        {
            
        }
    }
}