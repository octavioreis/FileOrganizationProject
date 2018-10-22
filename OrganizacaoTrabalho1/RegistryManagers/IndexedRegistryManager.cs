using OrganizacaoTrabalho1.Models;
using System;
using System.IO;
using System.Text;

namespace OrganizacaoTrabalho1.RegistryManagers
{
    public sealed class IndexedRegistryManager : AbstractRegistryManager
    {
        private const int _dataRegistrySize = 22;
        private const int _indexRegistrySize = 8;

        public string IndexFile { get; }

        public IndexedRegistryManager(string registryFile, string indexFile) : base(registryFile)
        {
            if (!File.Exists(indexFile))
                throw new IOException($"Invalid '{nameof(indexFile)}'.");

            IndexFile = indexFile;
        }

        public override DataRegistry FetchRegistry(int registryNumber)
        {
            return BinarySearchInIndexFile(registryNumber);
        }

        private static T BinarySearch<T>(
            FileStream stream,
            int id,
            int min,
            int max,
            int registrySize,
            Func<string, T> deserializer) where T : class, IRegistry
        {
            if (min > max)
                return null;

            var middle = (min + max) / 2;

            stream.Seek(middle * registrySize, SeekOrigin.Begin);

            var registry = deserializer(new StreamReader(stream).ReadLine());

            if (registry.Id == id)
            {
                return registry;
            }
            else if (id < registry.Id)
            {
                return BinarySearch(stream, id, min, middle - 1, registrySize, deserializer);
            }
            else
            {
                return BinarySearch(stream, id, middle + 1, max, registrySize, deserializer);
            }
        }

        private DataRegistry BinarySearchInDataFile(int id)
        {
            using (var stream = File.OpenRead(base.RegistryFile))
            {
                return BinarySearch(stream, id, 0, (int)stream.Length / _dataRegistrySize, _dataRegistrySize, RegistrySerializer.DeserializeDataRegistry);
            }
        }

        private DataRegistry BinarySearchInIndexFile(int id)
        {
            IndexRegistry indexRegistry = null;

            using (var stream = File.OpenRead(IndexFile))
            {
                indexRegistry = BinarySearch(stream, id, 0, (int)stream.Length / _indexRegistrySize, _indexRegistrySize, RegistrySerializer.DeserializeIndexRegistry);
            }

            if (indexRegistry == null)
                return null;

            using (var dataStreamReader = new StreamReader(base.RegistryFile))
            {
                dataStreamReader.BaseStream.Seek((indexRegistry.DataIndex - 1) * _dataRegistrySize, SeekOrigin.Begin);

                return RegistrySerializer.DeserializeDataRegistry(dataStreamReader.ReadLine());
            }
        }
    }
}
