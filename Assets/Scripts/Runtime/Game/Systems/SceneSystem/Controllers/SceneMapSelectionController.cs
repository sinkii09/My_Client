using Nara.Game;
using Nara.Game.Enum;
using Nara.Patterns;
using Nara.System.Map;
using Nara.System.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.System.Scene
{
    public class SceneMapSelectionController : SceneControllerBase
    {
        public override SceneEnum SceneType => SceneEnum.MapSelection;
        private EventBus _eventBusSystem;
        private UISystem _uiSystem;
        private MapSystem _mapSystem;

        private List<IMap> _maps = new List<IMap>();
        protected override void LateStart()
        {
            _mapSystem = GameApp.Inteface.GetSystem<MapSystem>();
            DisplayMaps();
        }

        public void DisplayMaps()
        {
            Debug.Log("Displaying maps");
            Debug.Log($"Maps count: {_mapSystem.Maps.Count}");
            foreach (var map in _mapSystem.Maps)
            {
                var obj = new GameObject();
                Debug.Log($"Map: {map.Value.MapData.MapType}");
            }
        }
    }
}