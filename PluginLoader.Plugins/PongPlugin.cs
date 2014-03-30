using System;

namespace PluginLoader.Plugins
{
    using System.ComponentModel.Composition;

    [Export(typeof(IPlugin))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ExportMetadata("PluginType", "PongPlugin")]
    [ExportMetadata("PluginPurpose", "Game")]
    public class PongPlugin : IPlugin
    {
        public Guid PluginId { get; private set; }

        public PongPlugin()
        {
            this.PluginId = Guid.NewGuid();
        }
    }
}
