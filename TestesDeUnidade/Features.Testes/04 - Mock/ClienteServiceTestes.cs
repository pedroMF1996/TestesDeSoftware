using Features.Clientes;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Testes
{
    public class ClienteServiceTestes : IClassFixture<ClienteBogusFixture>
    {
        readonly ClienteBogusFixture _clienteBogusFixture;

        public ClienteServiceTestes(ClienteBogusFixture clienteBogusFixture)
        {
            _clienteBogusFixture = clienteBogusFixture;
        }

        [Fact(DisplayName ="Adicionar Cliente com Sucesso")]
        [Trait("Categoria","Cliente Service Mock Testes")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            //Arange
            var cliente = _clienteBogusFixture.GerarClienteValido();
            
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();
            
            var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);


            //Act
            clienteService.Adicionar(cliente);

            
            //Assert
            Assert.True(cliente.EhValido());
            clienteRepo.Verify(r => r.Adicionar(cliente), Times.Once);
            mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None),Times.Once);
        }
        
        [Fact(DisplayName ="Adicionar Cliente com Falha")]
        [Trait("Categoria","Cliente Service Mock Testes")]
        public void ClienteService_Adicionar_DeveExecutarInvalido()
        {

        }
        
        [Fact(DisplayName ="Obter Cliente com Sucesso")]
        [Trait("Categoria","Cliente Service Mock Testes")]
        public void ClienteService_Obter_DeveExecutarComSucesso()
        {

        }

    }
}
