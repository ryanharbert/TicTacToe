using TMPro;
using UnityEngine;

namespace TicTacToe
{
    public class GameTextDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private Color duringGame;
        [SerializeField] private Color endGame;
        
        private void Awake()
        {
            GameManager.GameOver += GameOver;
            GameManager.TurnStarted += TurnStarted;
            
            text.color = duringGame;
            text.text = "Tic Tac Toe";
        }

        private void OnDestroy()
        {
            GameManager.GameOver -= GameOver;
            GameManager.TurnStarted -= TurnStarted;
        }

        void GameOver(OutcomeData data)
        {
            text.color = endGame;
            if (data.Winner == PieceType.O)
            {
                text.text = "O Wins";
            }
            else if(data.Winner == PieceType.X)
            {
                text.text = "X Wins";
            }
            else
            {
                text.text = "Draw";
            }
        }

        void TurnStarted(PieceType type)
        {
            text.color = duringGame;
            if (type == PieceType.O)
            {
                text.text = "O's Turn";
            }
            else
            {
                text.text = "X's Turn";
            }
        }
    }
}