using OrganizacaoTrabalho1.Models;

namespace OrganizacaoTrabalho1
{
    public static class RegistrySerializer
    {
        public static DataRegistry DeserializeDataRegistry(string registryString)
        {
            return new DataRegistry
            {
                Id = int.Parse(registryString.Substring(0, 4)),
                Name = registryString.Substring(4, 9).Trim(),
                Age = int.Parse(registryString.Substring(13, 2)),
                Salary = int.Parse(registryString.Substring(15, 4))
            };
        }

        public static IndexRegistry DeserializeIndexRegistry(string registryString)
        {
            return new IndexRegistry
            {
                Id = int.Parse(registryString.Substring(0, 4)),
                DataIndex = int.Parse(registryString.Substring(4, 2))
            };
        }

        public static DirectAccessDataRegistry DeserializeDADataRegistry(string registryString)
        {
            return new DirectAccessDataRegistry
            {
                Id = int.Parse(registryString.Substring(0, 4)),
                Name = registryString.Substring(4, 9).Trim(),
                Age = int.Parse(registryString.Substring(13, 2)),
                Salary = int.Parse(registryString.Substring(15, 4)),
                ELO = int.Parse(registryString.Substring(19, 1)),
            };
        }
    }
}
