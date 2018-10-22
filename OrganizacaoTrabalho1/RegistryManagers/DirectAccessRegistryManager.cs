using OrganizacaoTrabalho1.Models;
using System.IO;

namespace OrganizacaoTrabalho1.RegistryManagers
{
    public class DirectAccessRegistryManager : AbstractRegistryManager
    {
        private const int _dataRegistrySize = 23;

        public string ExtensionFile { get; }

        public DirectAccessRegistryManager(string registryFile, string extensionFile) : base(registryFile)
        {
            ExtensionFile = extensionFile;
        }

        public override DataRegistry FetchRegistry(int registryNumber)
        {
            return SearchInDataFile(HashFunction(registryNumber), registryNumber);
        }

        private DataRegistry SearchInDataFile(int directIndex, int registryNumber)
        {
            using (var dataStreamReader = new StreamReader(base.RegistryFile))
            {
                dataStreamReader.BaseStream.Seek((directIndex - 1) * _dataRegistrySize, SeekOrigin.Begin);

                var registry = RegistrySerializer.DeserializeDADataRegistry(dataStreamReader.ReadLine());
            }

            return new DataRegistry() { Id = registryNumber };
        }

        private int HashFunction(int registryNumber)
        {
            return registryNumber / 100 % 10;
        }
    }
}
