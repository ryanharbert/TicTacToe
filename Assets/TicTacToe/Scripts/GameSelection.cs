using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe
{
    [RequireComponent(typeof(PlayerPlacement), typeof(RandomPlacement))]
    public class GameSelection : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown xDropdown;
        [SerializeField] private TMP_Dropdown oDropdown;
        [SerializeField] private Button restartButton;

        private PlayerPlacement player;
        private RandomPlacement random;
        
        private void Start()
        {
            player = GetComponent<PlayerPlacement>();
            random = GetComponent<RandomPlacement>();
            
            SetupDropdown(xDropdown);
            SetupDropdown(oDropdown);
            
            Restart();
            
            restartButton.onClick.AddListener(Restart);
        }

        void Restart()
        {
            GameManager.Instance.StartGame(GetPlacementFromDropdown(xDropdown), GetPlacementFromDropdown(oDropdown), new BasicGame());
        }

        void SetupDropdown(TMP_Dropdown dropdown)
        {
            dropdown.ClearOptions();
            List<string> options = new List<string>();
            options.Add("Player");
            options.Add("Random");
            dropdown.AddOptions(options);
            dropdown.value = 0;
        }

        PiecePlacement GetPlacementFromDropdown(TMP_Dropdown dropdown)
        {
            if (dropdown.value == 0)
            {
                return player;
            }
            else if (dropdown.value == 1)
            {
                return random;
            }

            return player;
        }
    }
}