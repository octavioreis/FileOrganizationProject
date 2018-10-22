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
        private const string _areaExtensaoAcessoDireto = @"..\..\Registros\acesso_direto\area_extensao.txt";

        static void Main(string[] args)
        {
            var sequencialRegistryManager = new IndexedRegistryManager(_registrosSequencialIndexado, _indiceSequencialIndexado);
            var indexedFilesRegistryManager = new IndexedRegistryManager(_registrosArquivosIndexado, _indiceArquivosIndexado);
            var directAccessRegistryManager = new DirectAccessRegistryManager(_registrosAcessoDireto, _areaExtensaoAcessoDireto);

            Testar(sequencialRegistryManager);
            Testar(indexedFilesRegistryManager);
            Testar(directAccessRegistryManager);
        }

        private static void Testar(AbstractRegistryManager registryManager)
        {
            var ids = new int[]
            {
                1000,
                1050,
                1075,
                1100,
                1300,
                1350,
                1400,
                1440,
                1480,
                1600,
                1700,
                1800,
                1850,
                1900,
                1950,
                1975,
                2000
            };

            foreach (var id in ids)
            {
                var registro = registryManager.FetchRegistry(id);
                if (registro == null || registro.Id != id)
                    throw new Exception();
            }
        }
    }
}
