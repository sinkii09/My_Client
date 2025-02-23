using Cysharp.Threading.Tasks;
using Nara.Core.Architecture;
using Nara.Game.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.Game.System
{
    public class ResourceSystem : BaseSystem
    {
        private const string AGENT_PATH = "SO/Agents";
        protected override bool OnInit()
        {
            return true;
        }

        public async UniTask<List<AgentData>> GetAgentData()
        {
            List<AgentData> agents = new List<AgentData>();

            await UniTask.SwitchToMainThread();

            AgentData[] data = Resources.LoadAll<AgentData>(AGENT_PATH);
            agents.AddRange(data);

            Debug.Log($"Loaded {agents.Count} agents");
            return agents;
        }
    }
}