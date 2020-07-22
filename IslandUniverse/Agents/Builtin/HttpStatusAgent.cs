using System;
using System.Net.Http;

namespace IslandUniverse.Agents.Builtin
{
    public class HttpStatusAgent : IAgent
    {
        private readonly HttpClient http;

        public string Name { get; set; }

        [AgentEditable]
        public string Uri { get; set; }

        public string AgentTypeName => "HTTPStatusAgent";

        public string AgentDescription => "Returns the status code of a resource at the specified URL.";

        public HttpStatusAgent(HttpClient http)
        {
            this.http = http;
        }

        public dynamic Execute(dynamic input)
        {
            var uri = new Uri(Uri);
            var res = this.http.GetAsync(uri).GetAwaiter().GetResult();
            return res.StatusCode;
        }
    }
}
