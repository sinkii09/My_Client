using Nara.Core.Architecture;
using Nara.Game.Data;
using Nara.Game.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.Game.System
{
    public class AgentManagementSystem : BaseSystem
    {
        public List<AgentData> AllAgentsData { get; private set; } = new();

        private GlobalData userGlobalData;
        protected override bool OnInit()
        {
            userGlobalData = GameApp.Interface.GetModel<GlobalData>();
            return true;
        }
        public void UnlockAgent(AgentData data)
        {
            if (!AllAgentsData.Contains(data))
            {
                Debug.LogWarning("Agent is not existed!");
                return;
            }

            var id = Guid.NewGuid().ToString();
            IAgentModel agent = new AgentModel.Builder()
                .Identity.SetIdentity(id, data)
                .Stats.SetStats(1, 0, 0)
                .Build();

            userGlobalData.Add(agent);
        }
        public void RemoveAgent(IAgentModel agent)
        {
            userGlobalData.Remove(agent);
        }
        public async void LoadAllAgentDataAsync()
        {
            AllAgentsData = await GameApp.Interface.GetSystem<ResourceSystem>().GetAgentData();
            foreach (var agent in AllAgentsData)
            {
                Debug.Log(agent.Name);
            }
        }
    }
}