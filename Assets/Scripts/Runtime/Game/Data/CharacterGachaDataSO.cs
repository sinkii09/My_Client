using Nara.Game.Enum;
using Nara.Game.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.Game.Data
{
    [CreateAssetMenu(fileName = "CharacterGachaData", menuName = "Nara/CharacterGachaData")]
    public class CharacterGachaDataSO : ScriptableObject
    {
        public List<Character> Pool;
        public Dictionary<Rarity, float> RarityProbabilities = 
            new Dictionary<Rarity, float>
            {
                { Rarity.Common, 0.6f },
                { Rarity.Rare, 0.25f },
                { Rarity.Epic, 0.1f },
                { Rarity.SuperRare, 0.04f },
                { Rarity.UltraRare, 0.01f }
            };
    }
}