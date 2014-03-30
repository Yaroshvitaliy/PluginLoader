// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationKernel.cs" company="">
//   
// </copyright>
// <summary>
//   The application kernel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Plugin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    /// <summary>
    /// The application kernel.
    /// </summary>
    [Export]
    public class PluginHub<TPlugin>
        where TPlugin : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginHub"/> class.
        /// </summary>
        public PluginHub()
        {
            this.Container = new PluginContainer<TPlugin>();
        }

        public PluginContainer<TPlugin> Container { get; private set; }


        public IEnumerable<TPlugin> GetPlugins()
        {
            if (this.Container.Components == null)
            {
                return null;
            }

            return this.Container.Components.Select(c => c.Value);
        }

        public IEnumerable<TPlugin> GetPluginsByMetadata(string metadataKey, string metadataValue)
        {
            if (this.Container.Components == null)
            {
                return null;
            }

            return
                this.Container.Components.Where(
                    c => c.Metadata.ContainsKey(metadataKey) && c.Metadata[metadataKey].ToString() == metadataValue).Select(
                        c => c.Value);
        }

        public IEnumerable<TPlugin> GetPlugins(Func<IEnumerable<TPlugin>, IEnumerable<TPlugin>> getPlugins)
        {
            if (this.Container.Components == null)
            {
                return null;
            }

            if (getPlugins == null)
            {
                return null;
            }

            var plugins = getPlugins(this.Container.Components.Select(c => c.Value));

            return plugins;
        }
    }
}
