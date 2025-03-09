using Nara.Core.Architecture;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.Game.Model
{
    public class GlobalData : AbstractModel
    {
        public List<IAgentModel> Agents { get; private set; }
        protected override void OnInit()
        {
            Debug.Log("UserGlobalData Init");
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();
            Debug.Log("UserGlobalData Terminate");
        }
        public void Add(IAgentModel agent)
        {
            if (Agents.Contains(agent))
            {
                Debug.LogWarning("Agent is already existed in User Data!");
                return;
            }
            Agents.Add(agent);
        }
        public void Remove(IAgentModel agent)
        {
            if (!Agents.Contains(agent))
            {
                Debug.LogWarning("Agent is not existed in User Data or already be removed!");
                return;
            }
            Agents.Remove(agent);
        }
    }
}