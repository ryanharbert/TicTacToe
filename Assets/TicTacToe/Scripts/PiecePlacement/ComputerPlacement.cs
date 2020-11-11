using System.Collections;
using GridSystem;
using UnityEngine;

namespace TicTacToe
{
    public abstract class ComputerPlacement : PiecePlacement
    {
        public static float ThinkTime = 0.7f;

        private Coroutine fakeThinking;

        private bool thinking = false;
        
        protected override void OnStartPlacement()
        {
            fakeThinking = StartCoroutine(FakeThinking());
        }

        public override void Cleanup()
        {
            if (thinking)
            {
                StopCoroutine(fakeThinking);
            }
        }

        IEnumerator FakeThinking()
        {
            thinking = true;

            yield return new WaitForSeconds(ThinkTime);
            
            PlacePiece(ComputerSelectPiece());

            thinking = false;
        }

        protected abstract Vector2Int ComputerSelectPiece();
    }
}