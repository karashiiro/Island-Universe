namespace IslandUniverse.Agents
{
    public interface IAgent
    {
        /// <summary>
        ///     The name of this instance of the agent.
        /// </summary>
        [AgentEditable]
        string Name { get; set; }

        /// <summary>
        ///     The formatted display name of this type of agent.
        /// </summary>
        [AgentMetadata]
        string AgentTypeName { get; }

        /// <summary>
        ///     A description of what this type of agent does.
        /// </summary>
        [AgentMetadata]
        string AgentDescription { get; }

        /// <summary>
        ///     The operative function of the agent.
        /// </summary>
        /// <param name="input">Any object recieved from a previous agent in a procedure.</param>
        /// <returns>Any object that should be passed onto a future agent.</returns>
        dynamic Execute(dynamic input);
    }
}
