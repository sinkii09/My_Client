using Nara.Game.Enum;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.System.Map
{
    [Serializable]
    [CreateAssetMenu(fileName = "MapData", menuName = "Nara/System/MapData")]
    public class MapData : ScriptableObject
    {
        #region Data
        [field: SerializeField]
        public MapType MapType { get; private set; }

        [field: SerializeField]
        public string MapName { get; private set; }

        [field: SerializeField]
        public bool IsLocked { get; private set; }

        [field: SerializeField]
        public List<Wave> Waves { get; private set; }
        #endregion

        #region Visualizer
        [field: SerializeField]
        public Sprite MapSprite { get; private set; }
        #endregion
    }
}