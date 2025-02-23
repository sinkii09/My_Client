using Nara.Core.Architecture;
using Nara.Game.Command;
using Nara.Game.Enum;
using Nara.Game.Unit;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.Game.System
{
    public class GachaSystem : BaseSystem
    {
        private List<Character> _characterPool;
        private Dictionary<Rarity, float> _rarityProbabilities;
        private Dictionary<int, float> _rateUpProbabilities;
        private int _pityThreshold;
        private int _rollCount;
        protected override bool OnInit()
        {
            Debug.Log("Init Gacha System");
            return true;
        }
        public GachaCommandResult Gacha()
        {
            Character character = Roll();
            Debug.Log($"Gacha: {character.Data.Name}");
            return new GachaCommandResult(character.Data.Id);
        }
        public void Gacha10()
        {
            Debug.Log("Gacha10");
        }
        public void LoadCharacterPool(List<Character> characterPool, Dictionary<Rarity, float> rarityProbabilities,Dictionary<int,float> rate_up = null, int pityThreshold = 100, int rollCount = 0)
        {
            Debug.Log("Load Character Pool");
            _characterPool = characterPool;
            _rarityProbabilities = rarityProbabilities;
            _rateUpProbabilities = rate_up ?? new Dictionary<int, float>();
            _pityThreshold = pityThreshold;
            _rollCount = rollCount;

            Debug.Log($"Character Pool: {_characterPool.Count}");
            Debug.Log($"Rarity Probabilities: {_rarityProbabilities.Count}");
        }
        public void SetRateUp(int characterId, float rateUpProbabilty)
        {
            _rateUpProbabilities[characterId] = rateUpProbabilty;
        }
        private Character Roll()
        {
            _rollCount++;
            Rarity rarity = DetermineRarity();

            List<Character> availableCharacters = _characterPool.FindAll(c => c.Data.Rarity == rarity);
            Character rolledCharacter = DetermineCharacter(availableCharacters);

            Debug.Log($"Rolled a {rarity} character: {rolledCharacter.Data.Name}");
            return rolledCharacter;
        }
        private Rarity DetermineRarity()
        {
            if (_rollCount >= _pityThreshold)
            {
                _rollCount = 0;
                return Rarity.UltraRare;
            }

            float roll = UnityEngine.Random.value;
            float cumulativeProbability = 0f;

            foreach (var rarityProbability in _rarityProbabilities)
            {
                cumulativeProbability += rarityProbability.Value;
                if (roll < cumulativeProbability)
                {
                    return rarityProbability.Key;
                }
            }

            return Rarity.Common; // Fallback to Common if no other rarity is determined
        }
        private Character DetermineCharacter(List<Character> availableCharacters)
        {
            float totalProbability = 0f;
            foreach (var character in availableCharacters)
            {
                if (_rateUpProbabilities.TryGetValue(character.Data.Id, out float rateUpProbability))
                {
                    totalProbability += rateUpProbability;
                }
                else
                {
                    totalProbability += 1f; // Default probability for non-rate-up characters
                }
            }

            float roll = UnityEngine.Random.value * totalProbability;
            float cumulativeProbability = 0f;

            foreach (var character in availableCharacters)
            {
                if (_rateUpProbabilities.TryGetValue(character.Data.Id, out float rateUpProbability))
                {
                    cumulativeProbability += rateUpProbability;
                }
                else
                {
                    cumulativeProbability += 1f; // Default probability for non-rate-up characters
                }

                if (roll < cumulativeProbability)
                {
                    return character;
                }
            }

            return availableCharacters[0];
        }
    }
}