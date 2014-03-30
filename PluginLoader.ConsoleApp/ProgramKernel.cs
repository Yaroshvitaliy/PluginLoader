namespace PluginLoader.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Plugin;

    using PluginLoader.Plugins;

    public class ProgramKernel
    {
        private readonly PluginHub<IPlugin> pluginHub = new PluginHub<IPlugin>();

        public void TypeImports()
        {
            Console.WriteLine("Type Imports");

            this.pluginHub.Container.TypeImports(new[] { typeof(PingPlugin), typeof(PongPlugin) });

            this.PrintPlugins();
        }

        public void AssemblyImports()
        {
            Console.WriteLine("Assembly Imports");

            var assembly = Assembly.Load("PluginLoader.Plugins");
            this.pluginHub.Container.AssemblyImports(assembly);

            this.PrintPlugins();
        }

        public void DirectoryImports()
        {
            Console.WriteLine("Directory Imports");

            var pluginsDirName = "\\Plugins";

            var pluginsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + pluginsDirName;

            this.pluginHub.Container.DirectoryImports(pluginsPath);

            this.PrintPlugins();
        }

        public void StartGame()
        {
            var pingPlugins = this.pluginHub.GetPluginsByMetadata("PluginType", "PingPlugin").ToList();
            
            if (!pingPlugins.Any())
            {
                return;
            }
            
            pingPlugins.ForEach(p => p.Start());
        }

        private void PrintPlugins()
        {
            var plugins = this.pluginHub.GetPlugins();
            this.PrintPlugins(plugins);

            Console.WriteLine("Pings:");
            var pingPlugins = this.pluginHub.GetPluginsByMetadata("PluginType", "PingPlugin");
            this.PrintPlugins(pingPlugins);

            Console.WriteLine("Games:");
            var gamePlugins = this.pluginHub.GetPluginsByMetadata("PluginPurpose", "Game");
            this.PrintPlugins(gamePlugins);
        }

        private void PrintPlugins(IEnumerable<IPlugin> plugins)
        {
            if (plugins == null)
            {
                return;
            }

            Console.WriteLine();

            foreach (var plugin in plugins)
            {
                Console.WriteLine("Type: {0},\r\nId: {1}", plugin.GetType(), plugin.PluginId);
                Console.WriteLine();
            }
        }
    }
}
