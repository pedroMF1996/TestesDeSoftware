using Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Testes
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestesFixture>
    { }

    public class ClienteTestesFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var cliente = new Cliente(Guid.NewGuid(), 
                                     "Eduardo",
                                     "Pires", 
                                     DateTime.Now.AddYears(-30),
                                     "edu@edu.com", 
                                     true, 
                                     DateTime.Now);

            return cliente;
        }
        
        public Cliente GerarClienteInvalido()
        {
            var cliente = new Cliente(Guid.NewGuid(), 
                                      "",
                                      "", 
                                      DateTime.Now,
                                      "edu@edu.com", 
                                      true, 
                                      DateTime.Now);

            return cliente;
        }
        public void Dispose()
        {
        }
    }
}
