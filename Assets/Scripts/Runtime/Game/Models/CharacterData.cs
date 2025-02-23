using Nara.Core.Architecture;
using Nara.Game.Enum;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Nara.Game.Model
{
    [Serializable]
    public class CharacterData : AbstractModel
    {
        [ShowInInspector]
        public int Id { get; private set; }
        [ShowInInspector]
        public string Name { get; private set; }
        [ShowInInspector]
        public Rarity Rarity { get; private set; }

        protected override void OnInit()
        {
            Debug.Log("CharacterData OnInit");
        }
    }
}