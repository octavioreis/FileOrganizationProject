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

            //int registro;
            //if (registryManager.FetchRegistry(registro = 1000) == null ||
            //    registryManager.FetchRegistry(registro = 1050) == null ||
            //    registryManager.FetchRegistry(registro = 1075) == null ||
            //    registryManager.FetchRegistry(registro = 1100) == null ||
            //    registryManager.FetchRegistry(registro = 1300) == null ||
            //    registryManager.FetchRegistry(registro = 1350) == null ||
            //    registryManager.FetchRegistry(registro = 1400) == null ||
            //    registryManager.FetchRegistry(registro = 1440) == null ||
            //    registryManager.FetchRegistry(registro = 1480) == null ||
            //    registryManager.FetchRegistry(registro = 1600) == null ||
            //    registryManager.FetchRegistry(registro = 1700) == null ||
            //    registryManager.FetchRegistry(registro = 1800) == null ||
            //    registryManager.FetchRegistry(registro = 1850) == null ||
            //    registryManager.FetchRegistry(registro = 1900) == null ||
            //    registryManager.FetchRegistry(registro = 1950) == null ||
            //    registryManager.FetchRegistry(registro = 1975) == null ||
            //    registryManager.FetchRegistry(registro = 2000) == null)
            //{
            //}

            //registryManager.ReadAndWriteRegistries();
        }
    }
}
