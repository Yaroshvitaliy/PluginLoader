using System;

namespace PluginLoader.Plugins
{
    using System.ComponentModel.Composition;

    [Export(typeof(IPlugin))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ExportMetadata("PluginType", "PingPlugin")]
    [ExportMetadata("PluginPurpose", "Game")]
    public class PingPlugin: IPlugin
    {
        public Guid PluginId { get; private set; }

        public PingPlugin()
        {
            this.PluginId = Guid.NewGuid();
        }
    }
}
