using Nara.Core.Architecture;
using Nara.Game.Data;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Nara.Game.Model
{
    public interface IAgentModel : IModel
    {
        string ID { get; }
        AgentData Data { get; }
        int Level { get; }
        int Experience { get; }
        int Awakeness { get; }

    }
    [Serializable]
    public class AgentModel : AbstractModel, IAgentModel
    {
        [ShowInInspector]
        public string ID { get; private set; }

        [ShowInInspector]
        public AgentData Data { get; private set; }

        [ShowInInspector]
        public int Level { get; private set; }

        [ShowInInspector]
        public int Experience { get; private set; }

        [ShowInInspector]
        public int Awakeness { get; private set; }

        protected override void OnInit()
        {

        }

        public class Builder
        {
            private readonly AgentModel _agentModel;

            public Builder()
            {
                _agentModel = new AgentModel();
            }
            public IdentityBuilder Identity => new IdentityBuilder(_agentModel,this);
            public StatsBuilder Stats => new StatsBuilder(_agentModel, this);
            public IAgentModel Build()
            {
                return _agentModel;
            }

            public class IdentityBuilder
            {
                private readonly AgentModel _agentModel;
                private readonly Builder _builder;
                private AgentModel agentModel;

                public StatsBuilder Stats => _builder.Stats;
                public IdentityBuilder(AgentModel agentModel, Builder builder)
                {
                    _agentModel = agentModel;
                    _builder = builder;
                }
                public IdentityBuilder SetIdentity(string id, AgentData data)
                {
                    _agentModel.ID = id;
                    _agentModel.Data = data;
                    return this;
                }
            }
            public class StatsBuilder
            {
                private readonly AgentModel _agentModel;
                private readonly Builder _builder;
                public StatsBuilder(AgentModel agentModel, Builder builder)
                {
                    _agentModel = agentModel;
                    _builder = builder;
                }
                public IAgentModel Build() => _builder.Build();

                public StatsBuilder SetStats(int level, int experience, int awakeness)
                {
                    _agentModel.Level = level;
                    _agentModel.Experience = experience;
                    _agentModel.Awakeness = awakeness;
                    return this;
                }
            }
        }
    }
}