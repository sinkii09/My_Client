using Nara.Core.Architecture;
using Nara.Game;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Nara.Patterns
{
    [CreateAssetMenu(fileName = "ServiceConfigSO", menuName = "Nara/Configs/ServiceConfigSO", order = 0)]
    public class ServiceConfigSO : ScriptableObject
    {
        [SerializeReference]
        private List<IModel> _models;
        [SerializeReference]
        private List<ISystem> _systems;

        public void RegisterServices()
        {
            RegisterModel();
            RegisterSystem();
        }
        private void RegisterModel()
        {
            foreach (var model in _models)
            {
                if (model != null)
                {
                    var instance = Activator.CreateInstance(model.GetType()) as IModel;
                    GameApp.Instance.RegisterService(instance);
                }
            }
        }
        private void RegisterSystem()
        {
            foreach (var system in _systems)
            {
                if (system == null) continue;
                var instance = Activator.CreateInstance(system.GetType());
                GameApp.Instance.RegisterService(instance);
            }
        }
    }
}

