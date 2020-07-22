﻿using System;
using System.Reflection;
using System.Runtime.Loader;

namespace IslandUniverse.Plugin
{
    // Mostly taken from https://docs.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support#plugin-with-library-dependencies
    public class PluginLoadContext : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver resolver;

        public PluginLoadContext(string pluginPath) : base(isCollectible: true)
        {
            this.resolver = new AssemblyDependencyResolver(pluginPath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            var assemblyPath = this.resolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }
            return null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            var assemblyPath = this.resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (assemblyPath != null)
            {
                return LoadUnmanagedDllFromPath(assemblyPath);
            }
            return IntPtr.Zero;
        }
    }
}
