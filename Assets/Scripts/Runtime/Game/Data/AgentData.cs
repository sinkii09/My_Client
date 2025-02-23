using Nara.Game.Enum;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Nara.Game.Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "AgentData", menuName = "Nara/Game/AgentData")]
    public class AgentData: ScriptableObject
    {
        public string AgentID;

        public string Name;

        public Sprite Avatar;

        public Rarity Rarity;
    }
}