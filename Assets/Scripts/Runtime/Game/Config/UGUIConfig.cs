using Nara.Game.Enum;
using Nara.System.UGUISystems;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.Game.Config
{
    [CreateAssetMenu(fileName = "UGUIConfig", menuName = "Nara/Configs/UGUIConfig")]
    public class UGUIConfig : BaseConfig
    {
        [OdinSerialize]
        public Dictionary<UIType, UIHandler> UIHandlers { get; private set; }
    }
}