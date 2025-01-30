using Nara.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Nara.Patterns
{
    public static class EventBusUtil
    {
        public static IReadOnlyList<Type> EventTypes { get; set; }
        public static IReadOnlyList<Type> EventBusTypes { get; set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            EventTypes = PredefinedAssemblyUtil.GetTypes(typeof(IEvent));
            EventBusTypes = InitializeAllBuses();
        }
        static List<Type> InitializeAllBuses()
        {
            List<Type> eventBusTypes = new List<Type>();

            var typedef = typeof(EventBus<>);
            foreach (var type in EventTypes)
            {
                var busType = typedef.MakeGenericType(type);
                eventBusTypes.Add(busType);

                Debug.Log($"Initialized EventBus<{type.Name}>");
            }

            return eventBusTypes;
        }
        public static void ClearAllBuses()
        {
            Debug.Log("Clearing all buses...");
            for (int i = 0; i < EventBusTypes.Count; i++)
            {
                var busType = EventBusTypes[i];
                var clearMethod = busType.GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic);
                clearMethod?.Invoke(null, null);
            }
        }
    }
}

