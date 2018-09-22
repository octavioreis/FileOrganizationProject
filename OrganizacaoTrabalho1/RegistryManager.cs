using OrganizacaoTrabalho1.Models;
using System;
using System.IO;
using System.Text;

namespace OrganizacaoTrabalho1
{
    public sealed class RegistryManager
    {
        private readonly string _registryFile;
        private readonly string _indexFile;
        private const int _bufferSize = 128;
        private const int _dataRegistrySize = 22;
        private const int _indexRegistrySize = 8;

        public RegistryManager(string registryFile, string indexFile)
        {
            if (!File.Exists(registryFile))
                throw new IOException($"Invalid '{nameof(registryFile)}'.");

            if (!File.Exists(indexFile))
                throw new IOException($"Invalid '{nameof(indexFile)}'.");

            _registryFile = registryFile;
            _indexFile = indexFile;
        }

        public DataRegistry FetchRegistry(int registryNumber)
        {
            return BinarySearchInIndexFile(registryNumber);
        }

        public void WriteRegistries()
        {
            using (var fileStream = File.OpenRead(_registryFile))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, _bufferSize))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    WriteRegistry(RegistrySerializer.DeserializeDataRegistry(line));
                }
            }
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

        private static void WriteRegistry(DataRegistry registry)
        {
            Console.WriteLine($"Numero: {registry.Id} Nome: {registry.Name} Idade: {registry.Age} Salario: {registry.Salary}");
        }

        private DataRegistry BinarySearchInDataFile(int id)
        {
            using (var stream = File.OpenRead(_registryFile))
            {
                return BinarySearch(stream, id, 0, (int)stream.Length / _dataRegistrySize, _dataRegistrySize, RegistrySerializer.DeserializeDataRegistry);
            }
        }

        private DataRegistry BinarySearchInIndexFile(int id)
        {
            IndexRegistry indexRegistry = null;

            using (var stream = File.OpenRead(_indexFile))
            {
                indexRegistry = BinarySearch(stream, id, 0, (int)stream.Length / _indexRegistrySize, _indexRegistrySize, RegistrySerializer.DeserializeIndexRegistry);
            }

            if (indexRegistry == null)
                return null;

            using (var dataStreamReader = new StreamReader(_registryFile, Encoding.UTF8, false, _bufferSize))
            {
                dataStreamReader.BaseStream.Seek((indexRegistry.DataIndex - 1) * _dataRegistrySize, SeekOrigin.Begin);

                return RegistrySerializer.DeserializeDataRegistry(dataStreamReader.ReadLine());
            }
        }
    }
}
