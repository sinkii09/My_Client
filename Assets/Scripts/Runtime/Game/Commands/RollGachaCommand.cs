using Nara.Core;
using Nara.Core.Architecture;
using Nara.Game.System;
using UnityEngine;

namespace Nara.Game.Command
{
    public class RollGachaCommand : ICommand<GachaCommandResult>
    {

        public GachaCommandResult Execute()
        {
            return GameApp.Inteface.GetSystem<GachaSystem>().Gacha();
        }
    }
    public class GachaCommandResult
    {
        public int ResultCharId { get; private set; }

        public GachaCommandResult(int resultCharId)
        {
            ResultCharId = resultCharId;
        }
    }
}