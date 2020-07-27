namespace IslandUniverse.Agents
{
    public abstract class AgentBase
    {
        /// <summary>
        ///     The name of this instance of the agent.
        /// </summary>
        [AgentEditable]
        public string Name { get; set; }

        /// <summary>
        ///     The formatted display name of this type of agent.
        /// </summary>
        [AgentMetadata]
        public static string AgentTypeName { get; }

        /// <summary>
        ///     The icon for this agent.
        /// </summary>
        [AgentMetadata]
        public static string AgentIcon { get; }

        /// <summary>
        ///     A description of what this type of agent does.
        /// </summary>
        [AgentMetadata]
        public static string AgentDescription { get; }

        /// <summary>
        ///     The operative function of the agent.
        /// </summary>
        /// <param name="input">Any object recieved from a previous agent in a procedure.</param>
        /// <returns>Any object that should be passed onto a future agent.</returns>
        public abstract dynamic Execute(dynamic input);
    }
}
