using Nara.Core.Architecture;
using Nara.Game;
using Nara.System.Turn;
using UnityEngine;

namespace Nara.System.Combat
{
    public class CombatSystem : BaseSystem
    {
        TurnSystem _turnSystem => GameApp.Inteface.GetSystem<TurnSystem>();

        protected override bool OnInit()
        {
            
            return true;
        }
        public void StartCombat()
        {
            _turnSystem.StartTrackingTurn(TurnType.Player);
        }
        public void ChangeTurn()
        {
            _turnSystem.EndCurrentTurn(false);
        }
        public void EndCombat()
        {
            _turnSystem.EndCurrentTurn(true);
        }

        #region Bot Do Turn
        #endregion
    }
    public class CombatData
    {
        public int Allies { get; private set; }
        public int Enemies { get; private set; }


    }
}