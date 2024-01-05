using Features.Testes;
using NerdStore.WebApp.MVC;
using NerdStore.WebApp.MVC.Models;
using NerStore.Integrations.Tests.Config;
using System.Net.Http.Json;

namespace NerdStore.WebApp.Tests
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    [TestCaseOrderer("Features.Testes.PriorityOrderer", "Features.Testes")]
    public class PedidoApiTests: IClassFixture<IntegrationTestsFixture<Program>>
    {
        private readonly IntegrationTestsFixture<Program> _testsFixture;

        public PedidoApiTests(IntegrationTestsFixture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar item em novo pedido"), TestPriority(1)]
        [Trait("Categoria", "Integracao API - Pedidos")]
        public async Task AdicionarItem_NovoPedido_DeveRetornarComSucesso()
        {
            // Arrange
            var produtoId = new Guid("6ecaaa6b-ad9f-422c-b3bb-6013ec27c4bb");
            var itemInfo = new ItemViewModel()
            {
                Id = produtoId,
                Quantidade = 2,
            };

            await _testsFixture.RealizarLoginApi();

            _testsFixture.HttpClient.AtribuirToken(_testsFixture.UserToken);

            // Act
            var postResponse = await _testsFixture.HttpClient.PostAsJsonAsync("api/carrinho", itemInfo);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }    


        [Fact(DisplayName = "Remover Item em Pedido Existente"),TestPriority(2)]
        [Trait("Categoria", "Integracao API - Pedidos")]
        public async Task RemoverItem_PedidoExistente_DeveRetornarComSucesso()
        {
            // Arrange
            var produtoId = new Guid("6ecaaa6b-ad9f-422c-b3bb-6013ec27c4bb");
            await _testsFixture.RealizarLoginApi();
            _testsFixture.HttpClient.AtribuirToken(_testsFixture.UserToken);

            // Act
            var postResponse = await _testsFixture.HttpClient.DeleteAsync($"api/carrinho/{produtoId}");

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }    
    }
}