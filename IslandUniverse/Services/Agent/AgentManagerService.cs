using IslandUniverse.Agents;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IslandUniverse.Services.Agent
{
    public class AgentManagerService : Collection<IAgent>, IAgentManagerService
    {
        public IEnumerable<IAgent> Agents => this.ToList();
    }
}
