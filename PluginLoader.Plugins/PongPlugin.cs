using System;

namespace PluginLoader.Plugins
{
    using System.ComponentModel.Composition;
    using System.Threading.Tasks;

    using AsyncEventAggregator;

    [Export(typeof(IPlugin))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ExportMetadata("PluginType", "PongPlugin")]
    [ExportMetadata("PluginPurpose", "Game")]
    public class PongPlugin : IPlugin
    {
        public Guid PluginId { get; private set; }

        public void Start()
        {
            this.Publish(new Pong { Sender = this.PluginId, Message = "Pong!" }.AsTask());
        }

        public PongPlugin()
        {
            this.PluginId = Guid.NewGuid();
            this.Subscribe<Ping>(
                async p =>
                    {
                        Console.WriteLine(p.Result.Message);
                        await Task.Delay(500);
                        await this.Publish(new Pong { Sender = this.PluginId, Message = "Pong!" }.AsTask());
                    });
        }
    }
}
