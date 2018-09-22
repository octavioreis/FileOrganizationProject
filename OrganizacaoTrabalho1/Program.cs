using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizacaoTrabalho1
{
    class Program
    {
        static void Main(string[] args)
        {
            var registryManager = new RegistryManager(@"..\..\registros.txt", @"..\..\indice.txt");

            //var teste = registryManager.FetchRegistry(1600);

            //registryManager.ReadAndWriteRegistries();
        }
    }
}
