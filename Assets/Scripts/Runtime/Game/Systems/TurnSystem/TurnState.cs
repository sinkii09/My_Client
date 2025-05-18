using System;

namespace Nara.System.Turn
{
    public class TurnState
    {
        public const int MAX_TURN_COUNT = 10; // Example max turn count

        public event Action OnTurnStart;
        public event Action OnTurnEnd;

        public int TurnCount { get; private set; } 
        public TurnType CurrentState { get; private set; }
        public bool IsGameOver { get; private set; }
        public bool IsPlayerTurn => CurrentState == TurnType.Player;
        public bool IsEnemyTurn => CurrentState == TurnType.Enemy;
        public TurnState(TurnType state)
        {
            CurrentState = state;
            TurnCount = 0;
            IsGameOver = false;
        }
        public void StartTurn()
        {
            TurnCount++;
            OnTurnStart?.Invoke();
        }
        public void EndTurn(bool isGameOver)
        {
            OnTurnEnd?.Invoke();
            IsGameOver = isGameOver;
            SwitchTurn();
        }

        private void SwitchTurn()
        {
            if (IsGameOverCheck())
            {
                return;
            }

            if (IsPlayerTurn)
            {
                CurrentState = TurnType.Enemy;
            }
            else
            {
                CurrentState = TurnType.Player;
            }

            StartTurn();
        }
        public bool IsGameOverCheck()
        {
            if (TurnCount >= MAX_TURN_COUNT)
            {
                IsGameOver = true;
            }
            return IsGameOver;
        }
    }
}