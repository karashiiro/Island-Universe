using IslandUniverse.Agents;

namespace IslandUniverse.Services.Agent
{
    public interface IAgentManagerService
    {
        public int Count { get; }

        public void Add(AgentBase item);

        public AgentBase this[int i] { get; set; }
    }
}
