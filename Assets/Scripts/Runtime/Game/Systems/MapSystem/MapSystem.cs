using Nara.Core.Architecture;
using Nara.System.Map;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.System.Map
{
    public enum MapType
    {
        Map1,
        Map2,
        Map3,
    }
    public interface IMap
    {
        MapData MapData { get; }
        List<Wave> Waves { get; }

        bool IsCompleted { get; }
        bool IsLocked { get; }
        Wave CurrentWave { get; }
        
        void SetCurrentWave(int waveIndex);
        void UnLock();
        void Complete();
    }
    public class MapSystem : BaseSystem
    {
        public Dictionary<MapType,IMap> Maps { get; private set; } = new Dictionary<MapType, IMap>();
        private Dictionary<MapType, List<MapType>> _unlockMapping = new();
        public IMap CurrentMap { get; private set; }

        public event Action<IMap> OnMapSelected;
        public event Action<IMap> OnMapUnlocked;
        public event Action<IMap> OnMapCompleted;
        protected override bool OnInit()
        {
            LoadMaps();
            return true;
        }
        public void LoadMaps()
        {
            var mapDatas = Resources.LoadAll<MapData>("SO/Maps");
            if (mapDatas == null || mapDatas.Length == 0) return;

            foreach (var mapData in mapDatas)
            {
                var map = new Map(this, mapData, mapData.IsLocked); // TODO: apply save data
                Maps.TryAdd(map.MapData.MapType, map);
            }
        }
        public void SelectMap(IMap map)
        {
            if (map == null || !Maps.ContainsValue(map)) return;

            CurrentMap = map;

            OnMapSelected?.Invoke(CurrentMap);
        }
        public IMap GetMap(MapType mapType)
        {
            if (!Maps.ContainsKey(mapType)) return null;

            return Maps[mapType];
        }
        public void CompleteMap(IMap map)
        {
            if (!Maps.ContainsValue(map)) return;
            map.Complete();
            List<MapType> unlockMaps = _unlockMapping[map.MapData.MapType];
            foreach (var unlockMap in unlockMaps)
            {
                UnlockMap(unlockMap);
            }
            OnMapCompleted?.Invoke(map);
        }
        private void UnlockMap(MapType mapType)
        {
            if (!Maps.ContainsKey(mapType)) return;
            var unlockMap = Maps[mapType];
            unlockMap.UnLock();
            OnMapUnlocked?.Invoke(unlockMap);
        }
        public void LoadUnlockMapping()
        {
            _unlockMapping.Add(MapType.Map1, new List<MapType> { MapType.Map2 });
            _unlockMapping.Add(MapType.Map2, new List<MapType> { MapType.Map3 });
        }
    }
}