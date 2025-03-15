using System.Collections.Generic;

namespace Nara.System.Map
{
    public class Map : IMap
    {
        public MapData MapData { get; private set; }

        public List<Wave> Waves => MapData.Waves;

        public bool IsCompleted { get; set; }
        public bool IsLocked { get; private set; }

        public Wave CurrentWave
        {
            get
            {
                if (Waves == null || Waves.Count == 0) return null;
                return Waves[_currentWaveIndex];
            }
        }

        private int _currentWaveIndex = 0;

        private MapSystem _mapSystem;
        public Map(MapSystem mapSystem, MapData mapData, bool isLocked)
        {
            _mapSystem = mapSystem;
            _currentWaveIndex = 0;
            MapData = mapData;
            IsLocked = isLocked;
            SetCurrentWave(_currentWaveIndex);
        }
        public void SetCurrentWave(int waveIndex)
        {
            if (Waves == null || Waves.Count == 0) return;
            if (waveIndex < 0 || waveIndex >= Waves.Count) return;
            _currentWaveIndex = waveIndex;
        }
        public void UnLock()
        {
            IsLocked = false;
        }
        public void Complete() => IsCompleted = true;
    }
}