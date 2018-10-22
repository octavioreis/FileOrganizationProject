using OrganizacaoTrabalho1.Models;
using System;
using System.IO;

namespace OrganizacaoTrabalho1.RegistryManagers
{
    public abstract class AbstractRegistryManager
    {
        public string RegistryFile { get; }

        public AbstractRegistryManager(string registryFile)
        {
            if (!File.Exists(registryFile))
                throw new IOException($"Invalid '{nameof(registryFile)}'.");

            RegistryFile = registryFile;
        }

        public abstract DataRegistry FetchRegistry(int registryNumber);

        public void WriteRegistries()
        {
            using (var streamReader = new StreamReader(RegistryFile))
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
    }
}
