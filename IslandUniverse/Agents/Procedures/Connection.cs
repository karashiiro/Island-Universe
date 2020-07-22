using System.Collections.Generic;

namespace IslandUniverse.Agents.Procedures
{
    public class Connection
    {
        public IAgent Node { get; set; }

        public IEnumerable<Connection> Receivers { get; set; }

        public void Execute(dynamic input = null)
        {
            var output = Node.Execute(input);
            foreach (var receiver in Receivers)
            {
                receiver.Execute(output);
            }
        }
    }
}
