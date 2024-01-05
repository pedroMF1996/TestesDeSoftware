using AngleSharp.Html.Parser;
using FluentAssertions;
using NerdStore.WebApp.MVC;
using NerStore.Integrations.Tests.Config;

namespace NerdStore.WebApp.Tests
{
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class PedidoWebTests : IClassFixture<IntegrationTestsFixture<Program>>
    {
        private readonly IntegrationTestsFixture<Program> _testsFixture;

        public PedidoWebTests(IntegrationTestsFixture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar Item NovoPedido Deve Atualizar Valor Total")]
        [Trait("Categoria", "Integracao Web - Pedido")]
        public async Task AdicionarItem_NovoPedido_DeveAtualizarValorTotal()
        {
            // Arrange
            await _testsFixture.RealizarRegistrarUsuarioWeb();
            var produtoId = new Guid("6ecaaa6b-ad9f-422c-b3bb-6013ec27c4bb");
            var produtoDetalheUrl = $"/produto-detalhe/{produtoId}";
            const int quantidade = 2;

            var initialResponse = await _testsFixture.HttpClient.GetAsync(produtoDetalheUrl);
            initialResponse.EnsureSuccessStatusCode();
            
            var formData = new Dictionary<string, string>()
            {
                {"Id", produtoId.ToString() },
                {"quantidade", quantidade.ToString() }
            };

            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/meu-carrinho")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            var postResponse = await _testsFixture.HttpClient.SendAsync(postRequest);

            // Assert
            postResponse.EnsureSuccessStatusCode();

            var postResponseString = await postResponse.Content.ReadAsStringAsync();
            
            var html = new HtmlParser()
                            .ParseDocumentAsync(postResponseString)
                            .Result
                            .All;

            var formQuantidade = html?.FirstOrDefault(e => e.Id == "quantidade")?.GetAttribute("value").OnlyNumebrs();
            var formValorUnitario = html?.FirstOrDefault(e => e.Id == "valorUnitario")?.TextContent?.Split('.')[0].OnlyNumebrs();
            var formValorTotal = html?.FirstOrDefault(e => e.Id == "valorTotal")?.TextContent?.Split('.')[0].OnlyNumebrs();

            formQuantidade.Should().Be(2);
            formValorTotal.Should().Be(formQuantidade*formValorUnitario);
        }
    }
}