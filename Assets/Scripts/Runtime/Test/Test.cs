using UnityEngine;
using Sirenix.OdinInspector;
using Nara.Patterns;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Nara.Core;
using Nara.Core.Architecture;
public class Test : MonoBehaviour
{
    EventBinding<TestEvent> binding;

    private void OnEnable()
    {
        //binding = new EventBinding<TestEvent>(OnTestEvent);
        //EventBus<TestEvent>.Register(binding);
        EventBus<TestEvent>.BindingAndRegister(OnTestEvent);

    }
    [Button]
    public void Test_RaiseEvent()
    {
        EventBus<TestEvent>.Raise(new TestEvent
        {
            name = "Test 111111",
        }); 
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
