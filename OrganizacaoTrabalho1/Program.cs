using OrganizacaoTrabalho1.Models;
using OrganizacaoTrabalho1.RegistryManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizacaoTrabalho1
{
    class Program
    {
        private const string _registrosSequencialIndexado = @"..\..\Registros\sequencial_indexado\registros.txt";
        private const string _indiceSequencialIndexado = @"..\..\Registros\sequencial_indexado\indice.txt";

        private const string _registrosArquivosIndexado = @"..\..\Registros\arquivos_indexado\registros.txt";
        private const string _indiceArquivosIndexado = @"..\..\Registros\arquivos_indexado\indice.txt";

        private const string _registrosAcessoDireto = @"..\..\Registros\acesso_direto\registros.txt";

        static void Main(string[] args)
        {
            //var registryManager = new IndexedRegistryManager(_registrosSequencialIndexado, _indiceSequencialIndexado);
            var registryManager = new IndexedRegistryManager(_registrosArquivosIndexado, _indiceArquivosIndexado);

            Testar(registryManager);
        }

        private static void Testar(AbstractRegistryManager registryManager)
        {
            IRegistry registro;
            int idRegistro;
            if ((registro = registryManager.FetchRegistry(idRegistro = 1000)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1050)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1075)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1100)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1300)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1350)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1400)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1440)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1480)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1600)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1700)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1800)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1850)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1900)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1950)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 1975)) == null ||
                registro.Id != idRegistro ||
                (registro = registryManager.FetchRegistry(idRegistro = 2000)) == null)
            {
                throw new Exception();
            }
        }
    }
}
