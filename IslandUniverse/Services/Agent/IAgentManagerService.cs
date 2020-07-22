using IslandUniverse.Agents;
using System.Collections.Generic;

namespace IslandUniverse.Services.Agent
{
    public interface IAgentManagerService
    {
        /// <summary>
        ///     A copy of the set of all living agents.
        /// </summary>
        public IEnumerable<IAgent> Agents { get; }
    }
}
