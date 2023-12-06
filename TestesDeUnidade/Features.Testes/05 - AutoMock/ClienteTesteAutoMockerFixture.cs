using Bogus.DataSets;
using Bogus;
using Features.Clientes;
using Moq.AutoMock;

namespace Features.Testes
{
    [CollectionDefinition(nameof(ClienteAutoMockerCollection))]
    public class ClienteAutoMockerCollection : ICollectionFixture<ClienteTesteAutoMockerFixture>
    {}

    public class ClienteTesteAutoMockerFixture : IDisposable
    {

        public ClienteService ClienteService;
        public AutoMocker Mocker;



        public Cliente GerarClienteValido()
        {
            return GerarClientesValidos(1, true).FirstOrDefault();
        }

        public Cliente GerarClienteInvalido()
        {
            return GerarClientesInvalidos(1, false).FirstOrDefault();
        }

        public IEnumerable<Cliente> GerarClientesVariados()
        {
            var clientes = new List<Cliente>();

            clientes.AddRange(GerarClientesValidos(50, false).ToList());
            clientes.AddRange(GerarClientesValidos(50, true).ToList());

            return clientes;
        }

        public IEnumerable<Cliente> GerarClientesValidos(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Name.Gender>();
            var cliente = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                Guid.NewGuid(),
                        f.Name.FirstName(genero),
                        f.Name.LastName(genero),
                        f.Date.Past(80, DateTime.Now.AddYears(-18)),
                        "",
                        ativo,
                        DateTime.Now)
                )
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));

            return cliente.Generate(quantidade);
        }

        public IEnumerable<Cliente> GerarClientesInvalidos(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                Guid.NewGuid(),
                        f.Name.FirstName(genero),
                        f.Name.LastName(genero),
                        f.Date.Past(1, DateTime.Now.AddYears(1)),
                        "",
                        false,
                        DateTime.Now)
                );

            return cliente.Generate(quantidade);
        }

        public ClienteService ObterClienteService()
        {
            Mocker = new AutoMocker();
            ClienteService = Mocker.CreateInstance<ClienteService>();

            return ClienteService;
        }

        public void Dispose()
        {
            
        }
    }
}
