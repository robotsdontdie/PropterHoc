using System;
using System.Reflection;
using System.Runtime.Loader;

namespace PropterHoc.Plugins
{
    public class PluginLoadContext : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver dependencyResolver;

        public PluginLoadContext(string assemblyPath)
        {
            dependencyResolver = new AssemblyDependencyResolver(assemblyPath);
        }

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            string? assemblyPath = dependencyResolver.ResolveAssemblyToPath(assemblyName);
            if (assemblyPath is null)
            {
                return null;
            }

            return LoadFromAssemblyPath(assemblyPath);
        }

        protected override nint LoadUnmanagedDll(string unmanagedDllName)
        {
            string? libraryPath = dependencyResolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (libraryPath is null)
            {
                return IntPtr.Zero;
            }

            return LoadUnmanagedDllFromPath(libraryPath);
        }
    }
}