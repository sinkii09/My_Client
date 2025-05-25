using UnityEngine;
using Sirenix.OdinInspector;
using Nara.Patterns;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Nara.Core;
using Nara.Core.Architecture;
using Nara.Game;
using Nara.System;
public class Test : NaraBehaviour
{
    EventBinding<TestEvent> binding;

    private EventBus _eventBus;
    protected override void LateStart()
    {
        binding = new EventBinding<TestEvent>(OnTestEvent);

        _eventBus = GameApp.Interface.GetSystem<EventBus>();
        //_eventBus.Register(binding);
    }
    private void OnDestroy()
    {
        _eventBus.Unregister(binding);
    }
    [Button]
    public void Test_RaiseEvent()
    {
        //EventBus<TestEvent>.Raise(new TestEvent
        //{
        //    name = "Test 111111",
        //}); 
    }
    void OnTestEvent(TestEvent testEvent)
    {
        Debug.Log($"Even invoke {testEvent.name}");
        if (GameApp.Instance != null)
        {
            Debug.Log(testEvent.name);
        }
    }
    public class TestEvent : IEvent
    {
        public string name;
    }
}
