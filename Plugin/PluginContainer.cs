// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseContainer.cs" company="">
//   
// </copyright>
// <summary>
//   The base class for containers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Plugin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.IO;
    using System.Reflection;

    /// <summary>The base class for containers.</summary>
    /// <typeparam name="TPlugin">The plugin interface.</typeparam>
    public class PluginContainer<TPlugin>
        where TPlugin : class
    {
        private readonly AggregateCatalog aggregateCatalog = new AggregateCatalog();

        /// <summary>Gets or sets the components.</summary>
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<TPlugin, IDictionary<string, object>>> Components { get; set; }

        /// <summary>The current directory import.</summary>
        public void CurrentDirectoryImport()
        {
            this.DirectoryImports(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        /// <summary>The directory imports.</summary>
        /// <param name="directoryName">The directory name.</param>
        public void DirectoryImports(string directoryName)
        {
            var catalog = new DirectoryCatalog(directoryName);
            this.AggregateImports(catalog);
        }

        /// <summary>The assembly imports.</summary>
        /// <param name="assembly">The assembly.</param>
        public void AssemblyImports(Assembly assembly)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetExecutingAssembly();
            }

            var catalog = new AssemblyCatalog(assembly);
            this.AggregateImports(catalog);
        }

        public void TypeImport(Type type)
        {
            var catalog = new TypeCatalog(type);
            this.AggregateImports(catalog);
        }

        /// <summary>The type imports.</summary>
        /// <param name="types">The types.</param>
        public void TypeImports(params Type[] types)
        {
            var catalog = new TypeCatalog(types);
            this.AggregateImports(catalog);
        }

        /// <summary>The aggregate imports.</summary>
        private void AggregateImports(ComposablePartCatalog catalog)
        {
            aggregateCatalog.Catalogs.Add(catalog);
            this.ComposeParts(aggregateCatalog);
        }

        /// <summary>The compose parts.</summary>
        /// <param name="catalog">The catalog.</param>
        private void ComposeParts(ComposablePartCatalog catalog)
        {
            var container = new CompositionContainer(catalog, CompositionOptions.Default);
            var batch = new CompositionBatch();
            batch.AddPart(this);
            container.Compose(batch);
        }
    }
}
