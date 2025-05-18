using Nara.Core.Architecture;
using UnityEngine;

namespace Nara.System.Turn
{
    public class TurnSystem : BaseSystem
    {
        public TurnState CurrentTurn { get; private set; }

        protected override bool OnInit()
        {
            return true;
        }
        
        public void StartTrackingTurn(TurnType turnType)
        {
            if (CurrentTurn != null)
            {
                Debug.LogWarning("Turn tracking is already in progress.");
                return;
            }
            CurrentTurn = new TurnState(turnType);
            CurrentTurn.StartTurn();
        }
        public void EndCurrentTurn(bool isGameOver)
        {
            if (CurrentTurn == null)
            {
                Debug.LogWarning("No turn tracking in progress.");
                return;
            }

            CurrentTurn.EndTurn(isGameOver);

            if (CurrentTurn.IsGameOver)
            {
                CurrentTurn = null;
            }
        }
    }
}