using Bogus.DataSets;
using Bogus;
using Features.Clientes;
using static Bogus.DataSets.Name;

namespace Features.Testes
{
    [CollectionDefinition(nameof(ClienteBogusCollection))]
    public class ClienteBogusCollection : ICollectionFixture<ClienteBogusFixture>
    { }

    public class ClienteBogusFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            return GerarClientesValidos(1, true).FirstOrDefault();
        }

        #region Gerar Cliente Valido com explicacao

        //public Cliente GerarClienteValido()
        //{
        //    var genero = new Faker().PickRandom<Name.Gender>();
        //    //var email = new Faker().Internet.Email("eduardo", "pires", "gmail");

        //    //Para classes sem construtores
        //    //var clienteFaker = new Faker<Cliente>();
        //    //clienteFaker.RuleFor(c => c.Nome, (f,c)=>f.Name.FirstName());

        //    //Para classes com construtores
        //    var cliente = new Faker<Cliente>("pt_BR")
        //        .CustomInstantiator(f => new Cliente(
        //                Guid.NewGuid(),
        //                f.Name.FirstName(genero),
        //                f.Name.LastName(genero),
        //                f.Date.Past(80, DateTime.Now.AddYears(-18)),
        //                "",
        //                true,
        //                DateTime.Now)
        //        )
        //        .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));

        //    return cliente;
        //} 
        #endregion

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

        public void Dispose()
        {
        }
    }
}
