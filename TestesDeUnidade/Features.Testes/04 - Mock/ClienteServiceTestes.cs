﻿using Features.Clientes;
using MediatR;
using Moq;

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
            //Arange
            var cliente = _clienteBogusFixture.GerarClienteInvalido();

            var clienteRepo = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();

            var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);


            //Act
            clienteService.Adicionar(cliente);


            //Assert
            Assert.False(cliente.EhValido());
            clienteRepo.Verify(r => r.Adicionar(cliente), Times.Never);
            mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }
        
        [Fact(DisplayName ="Obter Clientes Ativos com Sucesso")]
        [Trait("Categoria","Cliente Service Mock Testes")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            //Arange
            var clienteRepo = new Mock<IClienteRepository>();
            var mediatr = new Mock<IMediator>();

            clienteRepo.Setup(r => r.ObterTodos())
                .Returns(_clienteBogusFixture.GerarClientesVariados());


            var clienteService = new ClienteService(clienteRepo.Object, mediatr.Object);

            //Act
            var clientes = clienteService.ObterTodosAtivos().ToList();

            //Assert
            clienteRepo.Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.All(clientes, c => Assert.True(c.Ativo));
        }

    }
}
