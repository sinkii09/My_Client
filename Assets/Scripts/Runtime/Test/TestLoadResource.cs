using Nara.Game;
using Nara.Game.System;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestLoadResource : MonoBehaviour
{
    [Button]
    public void LoadResource()
    {
        var agentManagementSystem = GameApp.Interface.GetSystem<AgentManagementSystem>();
        agentManagementSystem.LoadAllAgentDataAsync();
    }
}
