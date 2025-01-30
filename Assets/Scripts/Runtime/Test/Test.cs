using UnityEngine;
using Sirenix.OdinInspector;
using Nara.Patterns;
public class Test : MonoBehaviour
{
    EventBinding<TestEvent> binding;
    private void OnEnable()
    {
        binding = new EventBinding<TestEvent>(OnTestEvent);
        EventBus<TestEvent>.Register(binding);
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
    }
    public class TestEvent : IEvent
    {
        public string name;
    }
}
