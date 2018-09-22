using OrganizacaoTrabalho1.Models;
using System;
using System.IO;
using System.Text;

namespace OrganizacaoTrabalho1
{
    public class RegistryManager
    {
        private const int _bufferSize = 128;
        private const int _dataRegistrySize = 22;
        private readonly string _registryFile;
        private readonly string _indexFile;
        private readonly RegistrySerializer _serializer = new RegistrySerializer();

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
            return BinarySearchInDataFile(registryNumber);
        }

        public void ReadAndWriteRegistries()
        {
            using (var fileStream = File.OpenRead(_registryFile))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, _bufferSize))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    WriteRegistry(_serializer.DeserializeDataRegistry(line));
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
                return BinarySearch(streamReader, id, 0, (int)streamReader.BaseStream.Length / _dataRegistrySize);
            }
        }

        private DataRegistry BinarySearchInIndexFile(int id)
        {
            using (var streamReader = new StreamReader(_registryFile, Encoding.UTF8, false, _bufferSize))
            {
                return BinarySearch(streamReader, id, 0, (int)streamReader.BaseStream.Length / _dataRegistrySize);
            }
        }

        private DataRegistry BinarySearchInDataFile(StreamReader streamReader, int id, int min, int max)
        {
            if (min <= max)
            {
                var middle = (min + max) / 2;

                streamReader.BaseStream.Seek(middle * _dataRegistrySize, SeekOrigin.Begin);

                var registry = _serializer.DeserializeDataRegistry(new StreamReader(streamReader.BaseStream).ReadLine());

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

        private DataRegistry BinarySearch(StreamReader streamReader, int id, int min, int max)
        {
            if (min <= max)
            {
                var middle = (min + max) / 2;

                streamReader.BaseStream.Seek(middle * _dataRegistrySize, SeekOrigin.Begin);

                var registry = _serializer.DeserializeDataRegistry(new StreamReader(streamReader.BaseStream).ReadLine());

                if (registry.Id == id)
                {
                    return registry;
                }
                else if (id < registry.Id)
                {
                    return BinarySearch(streamReader, id, min, middle - 1);
                }
                else
                {
                    return BinarySearch(streamReader, id, middle + 1, max);
                }
            }

            return null;
        }
    }
}
