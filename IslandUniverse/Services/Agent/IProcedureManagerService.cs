using IslandUniverse.Agents.Procedures;
using System.Collections.Generic;

namespace IslandUniverse.Services.Agent
{
    public interface IProcedureManagerService
    {
        /// <summary>
        ///     A copy of the set of all available procedures.
        /// </summary>
        public IEnumerable<Procedure> Procedures { get; }
    }
}
