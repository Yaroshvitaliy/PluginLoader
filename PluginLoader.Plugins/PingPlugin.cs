using System;

namespace PluginLoader.Plugins
{
    using System.ComponentModel.Composition;
    using System.Threading.Tasks;

    using AsyncEventAggregator;

    [Export(typeof(IPlugin))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [ExportMetadata("PluginType", "PingPlugin")]
    [ExportMetadata("PluginPurpose", "Game")]
    public class PingPlugin: IPlugin
    {
        public Guid PluginId { get; private set; }

        public void Start()
        {
            this.Publish(new Ping { Message = "Ping!" }.AsTask());
        }

        public PingPlugin()
        {
            this.PluginId = Guid.NewGuid();
            this.Subscribe<Pong>(
                async p =>
                    {
                        Console.WriteLine(p.Result.Message);
                        await Task.Delay(1000);
                        await this.Publish(new Ping { Message = "Ping!" }.AsTask());
                    }
                );
        }
    }
}
