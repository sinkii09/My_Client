using Nara.Core.Architecture;
using Nara.System.Combat;

namespace Nara.System.Event
{
    internal class TurnStartEvent : IEvent
    {
        public BattleEntity Entity { get; private set; }

        public TurnStartEvent(BattleEntity entity)
        {
            this.Entity = entity;
        }
    }
}