using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using PropterHocPluginBase;

namespace PropterHoc.Plugins
{
    public class PluginLoader
    {
        public const string PluginManifestFilename = ".phplugin";

        public IPropterHocPlugin[] LoadFromSearchPath(string searchPath)
        {
            var plugins = new List<IPropterHocPlugin>();
            foreach (string dir in Directory.EnumerateDirectories(searchPath))
            {
                var manifest = ReadManifest(dir);
                plugins.AddRange(LoadPlugins(Path.Combine(dir, manifest.EntryAssemblyName)));
            }

            return plugins.ToArray();
        }

        public IEnumerable<IPropterHocPlugin> LoadPlugins(string pluginAssemblyPath)
        {
            var loadContext = new PluginLoadContext(pluginAssemblyPath);
            var assembly = loadContext.LoadFromAssemblyPath(pluginAssemblyPath);
            var types = assembly.GetTypes();
            var where = types.Where(type => typeof(IPropterHocPlugin).IsAssignableFrom(type));
            return where.Select(type => (IPropterHocPlugin)Activator.CreateInstance(type)!);
        }

        public PluginManifest ReadManifest(string pluginDir)
        {
            string manifestPath = Path.Combine(pluginDir, PluginManifestFilename);
            var contents = File.ReadAllText(manifestPath);
            var dto = JsonConvert.DeserializeObject<PluginManifestDto>(contents)
                ?? throw new JsonSerializationException("Manifest contents cannot be null");
            return new PluginManifest(dto);
        }
    }

    public class PluginManifest
    {
        public PluginManifest(PluginManifestDto dto)
        {
            EntryAssemblyName = dto.EntryAssemblyName;
        }

        public string EntryAssemblyName { get; }
    }

    public class PluginManifestDto
    {
        [JsonProperty("entryAssemblyName")]
        [JsonRequired]
        public string EntryAssemblyName { get; set; }
    }
}
