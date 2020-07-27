﻿using System;
using System.Net.Http;

namespace IslandUniverse.Agents.Builtin
{
    public class HttpStatusAgent : AgentBase
    {
        private readonly HttpClient http;

        [AgentEditable]
        public string Uri { get; set; }

        public new string AgentTypeName => "HTTPStatusAgent";
        public new string AgentIcon => "";
        public new string AgentDescription => "Returns the status code of a resource at the specified URL.";

        public HttpStatusAgent(HttpClient http)
        {
            this.http = http;
        }

        public override dynamic Execute(dynamic input)
        {
            var uri = new Uri(Uri);
            var res = this.http.GetAsync(uri).GetAwaiter().GetResult();
            return res.StatusCode;
        }
    }
}
