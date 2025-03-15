using System.Collections.Generic;

namespace Nara.System.Map
{
    public class Wave
    {
        public int WaveIndex { get; private set; }
        public List<WaveData> WaveData { get; private set; }
        public Wave(int waveIndex, List<WaveData> waveDatas)
        {
            WaveIndex = waveIndex;
            WaveData = waveDatas;
        }
        public bool IsCompleted { get; private set; }
        public bool IsFinalWave
        {
            get
            {
                if (WaveData.Count == 0) return false;
                return WaveIndex == WaveData.Count - 1;
            }
        }
    }
}