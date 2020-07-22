using Mono.Options;
using System;
using System.Collections.Generic;

namespace IslandUniverse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string storageDirOverride = null;
            var flags = new OptionSet
            {
                { "s|storage-directory=", "Override the default storage directory.", v => storageDirOverride = v },
            };

            try
            {
                flags.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine($"IslandUniverse: {e.Message}");
                return;
            }

            var app = new IslandUniverse(storageDirOverride);
            app.Start();
        }
    }
}
