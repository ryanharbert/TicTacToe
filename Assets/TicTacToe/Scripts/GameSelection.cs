using System;
using UnityEngine;

namespace TicTacToe
{
    [RequireComponent(typeof(PlayerPlacement), typeof(RandomPlacement))]
    public class GameSelection : MonoBehaviour
    {
        
        private void Start()
        {
            PlayerPlacement player = GetComponent<PlayerPlacement>();
            RandomPlacement random = GetComponent<RandomPlacement>();
            
            GameManager.Instance.StartGame(player, random, new BasicGame());
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