namespace IslandUniverse.Agents.Procedures
{
    public class Procedure
    {
        /// <summary>
        ///     The time in seconds between procedure activations.
        /// </summary>
        public double IntervalSeconds { get; set; }

        /// <summary>
        ///     The root connection of this procedure.
        /// </summary>
        public Connection Root { get; set; }

        public void Execute() => Root.Execute();
    }
}
