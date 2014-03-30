namespace PluginLoader.Plugins
{
    using System;

    public interface ISubPlugin
    {
        Guid SubPluginId { get; set; }
        Guid PluginId { get; set; }
    }
}
