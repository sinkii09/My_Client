using Nara.Game.Model;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Nara.Game.Unit
{
    [Serializable]
    public class Character
    {
        [ShowInInspector]
        public CharacterData Data { get; private set; }
    
    }
}