﻿using OrganizacaoTrabalho1.Models;

namespace OrganizacaoTrabalho1
{
    public class RegistrySerializer
    {
        public DataRegistry DeserializeDataRegistry(string registryString)
        {
            return new DataRegistry
            {
                Id = int.Parse(registryString.Substring(0, 4)),
                Name = registryString.Substring(4, 9).Trim(),
                Age = int.Parse(registryString.Substring(13, 2)),
                Salary = int.Parse(registryString.Substring(15, 4))
            };
        }
    }
}