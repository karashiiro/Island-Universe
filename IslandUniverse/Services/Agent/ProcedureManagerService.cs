using IslandUniverse.Agents.Procedures;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IslandUniverse.Services.Agent
{
    public class ProcedureManagerService : Collection<Procedure>, IProcedureManagerService
    {
        public IEnumerable<Procedure> Procedures => this.ToList();
    }
}
