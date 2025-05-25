using Nara.Core.Architecture;
using Nara.Game;
using Nara.System.Event;
using Nara.System.Turn;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nara.System.Combat
{
    public enum CombatTeam
    {
        Player,
        Enemy,
    }

    public class CombatSystem : BaseSystem
    {
        TurnSystem _turnSystem => GameApp.Interface.GetSystem<TurnSystem>();


        private CombatData _combatData;

        public event Action OnCombatStarted;
        public event Action OnCombatEnded;
        public event Action<BattleEntity> OnTurnStart;
        public event Action<BattleEntity> OnTurnEnd;
        protected override bool OnInit()
        {

            return true;
        }
        public void StartCombat(CombatData data)
        {
            _combatData = data;
            OnCombatStarted?.Invoke();
            DoTurn();
        }
        public override void Update()
        {

        }

        private BattleEntity GetFastestCharacter()
        {
            _combatData.AllAlives.Sort((a, b) => b.CR.CompareTo(a.CR));
            return _combatData.AllAlives.FirstOrDefault();
        }

        private void DoTurn()
        {
            // Perform actions for the current entity's turn
            // This could involve attacking, using items, etc.
            // After the action is completed, change the turn

            // Get the fastest character
            BattleEntity entity = GetFastestCharacter();

            OnTurnStart?.Invoke(entity);
            GameApp.Interface.GetSystem<EventBus>().Raise(new TurnStartEvent(entity));
            if (entity.Team == CombatTeam.Player)
            {
                // Player's turn logic
                // For example, show UI for player to select action
            }
            else
            {
                EndTurn(entity);
                // Enemy's turn logic
                // For example, AI decides action
            }
        }
        public void EndTurn(BattleEntity entity)
        {
            OnTurnEnd?.Invoke(entity);
            DoTurn();
        }
        private bool IsGameOver()
        {
            if (_combatData.AliveAllies.Count == 0 || _combatData.AliveEnemies.Count == 0)
            {
                _combatData.FinishCombat(_combatData.AliveAllies.Count > 0);
                OnCombatEnded?.Invoke();
                return true;
            }
            return false;
        }
        #region Bot Do Turn
        #endregion
    }
    public class CombatData
    {
        public BattleEntity[] Allies { get; private set; }
        public BattleEntity[] Enemies { get; private set; }

        public List<BattleEntity> AliveAllies { get; private set; }
        public List<BattleEntity> AliveEnemies { get; private set; }

        public List<BattleEntity> AllAlives { get; private set; }
        public List<BattleEntity> AllDead { get; private set; }

        public bool IsGameOver { get; private set; }
        public bool IsWin { get; private set; }
        public CombatData(BattleEntity[] allies, BattleEntity[] enemies)
        {
            Allies = allies;
            Enemies = enemies;
            AliveAllies = new List<BattleEntity>(allies);
            AliveEnemies = new List<BattleEntity>(enemies);
            AllAlives = new List<BattleEntity>(allies);
            AllAlives.AddRange(enemies);
            AllDead = new List<BattleEntity>();
        }
        public void FinishCombat(bool isWin)
        {
            IsGameOver = true;
            IsWin = isWin;
        }
    }
    public class BattleEntity
    {
        public CombatTeam Team { get; private set; }
        public int CR { get; private set; }
    }
}