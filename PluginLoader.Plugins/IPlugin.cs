using System;

namespace PluginLoader.Plugins
{
    public interface IPlugin
    {
        Guid PluginId { get; }

        void Start();
    }
}
