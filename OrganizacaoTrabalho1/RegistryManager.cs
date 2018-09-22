using OrganizacaoTrabalho1.Models;
using System;
using System.IO;
using System.Text;

namespace OrganizacaoTrabalho1
{
    public class RegistryManager
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

        private static void WriteRegistry(DataRegistry registry)
        {
            Console.WriteLine($"Numero: {registry.Id} Nome: {registry.Name} Idade: {registry.Age} Salario: {registry.Salary}");
        }

        private DataRegistry BinarySearchInDataFile(int id)
        {
            using (var streamReader = new StreamReader(_registryFile, Encoding.UTF8, false, _bufferSize))
            {
                return BinarySearchInDataFile(streamReader, id, 0, (int)streamReader.BaseStream.Length / _dataRegistrySize);
            }
        }

        private DataRegistry BinarySearchInIndexFile(int id)
        {
            IndexRegistry indexRegistry = null;

            using (var indexStreamReader = new StreamReader(_indexFile, Encoding.UTF8, false, _bufferSize))
            {
                indexRegistry = BinarySearchInIndexFile(indexStreamReader, id, 0, (int)indexStreamReader.BaseStream.Length / _indexRegistrySize);
            }

            if (indexRegistry == null)
                return null;

            using (var dataStreamReader = new StreamReader(_registryFile, Encoding.UTF8, false, _bufferSize))
            {
                dataStreamReader.BaseStream.Seek((indexRegistry.DataIndex - 1) * _dataRegistrySize, SeekOrigin.Begin);

                return RegistrySerializer.DeserializeDataRegistry(dataStreamReader.ReadLine());
            }
        }

        private DataRegistry BinarySearchInDataFile(StreamReader streamReader, int id, int min, int max)
        {
            if (min <= max)
            {
                var middle = (min + max) / 2;

                streamReader.BaseStream.Seek(middle * _dataRegistrySize, SeekOrigin.Begin);

                var registry = RegistrySerializer.DeserializeDataRegistry(new StreamReader(streamReader.BaseStream).ReadLine());

                if (registry.Id == id)
                {
                    return registry;
                }
                else if (id < registry.Id)
                {
                    return BinarySearchInDataFile(streamReader, id, min, middle - 1);
                }
                else
                {
                    return BinarySearchInDataFile(streamReader, id, middle + 1, max);
                }
            }

            return null;
        }

        private IndexRegistry BinarySearchInIndexFile(StreamReader streamReader, int id, int min, int max)
        {
            if (min <= max)
            {
                var middle = (min + max) / 2;

                streamReader.BaseStream.Seek(middle * _indexRegistrySize, SeekOrigin.Begin);

                var registry = RegistrySerializer.DeserializeIndexRegistry(new StreamReader(streamReader.BaseStream).ReadLine());

                if (registry.Id == id)
                {
                    return registry;
                }
                else if (id < registry.Id)
                {
                    return BinarySearchInIndexFile(streamReader, id, min, middle - 1);
                }
                else
                {
                    return BinarySearchInIndexFile(streamReader, id, middle + 1, max);
                }
            }

            return null;
        }
    }
}
