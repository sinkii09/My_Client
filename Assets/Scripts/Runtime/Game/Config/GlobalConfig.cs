using Nara.Core.Architecture;
using Nara.Game;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.Game.Config
{
    [CreateAssetMenu(fileName = "ServiceConfigSO", menuName = "Nara/Configs/ServiceConfigSO", order = 0)]
    public class GlobalConfig : SerializedScriptableObject, IConfig
    {
        [SerializeField]
        private List<IModel> _models;
        [SerializeField]
        private List<ISystem> _systems;
        [SerializeField]
        private List<IConfig> _configs;

        public void RegisterServices()
        {
            RegisterConfig();
            RegisterModel();
            RegisterSystem();
        }
        private void RegisterModel()
        {
            if (_models == null || _models.Count == 0) return;
            foreach (var model in _models)
            {
                if (model == null) continue;
                GameApp.Instance.RegisterModel(model);
            }
        }
        private void RegisterSystem()
        {
            if (_systems == null || _systems.Count == 0) return;
            foreach (var system in _systems)
            {
                if (system == null) continue;
                GameApp.Instance.RegisterSystem(system);
            }
        }

        private void RegisterConfig()
        {
            if (_configs == null || _configs.Count == 0) return;
            foreach (var config in _configs)
            {
                if (config == null) continue;
                GameApp.Instance.RegisterConfig(config);
            }
        }
    }
}

