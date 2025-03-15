using System;
using UnityEngine;

namespace Nara.System.Map
{
    [Serializable]
    [CreateAssetMenu(fileName = "WaveData", menuName = "Nara/System/WaveData")]
    public class WaveData : ScriptableObject
    {
        [field: SerializeField]
        public int EnemyIndex { get; private set; }
        [field: SerializeField]
        public int EnemyCount { get; private set; }
    }
}