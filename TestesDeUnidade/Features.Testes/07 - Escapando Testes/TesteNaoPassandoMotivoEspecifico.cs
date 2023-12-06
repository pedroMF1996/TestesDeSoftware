using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Testes
{
    public class TesteNaoPassandoMotivoEspecifico
    {
        [Fact(DisplayName ="Novo Cliente 2.0", Skip = "Nova versao 2.0 quebrando")]
        [Trait("Categoria", "Escapando os testes")]
        public void Teste_NaoEstaPassando_VersaoNovaNaoCompativel()
        {
            Assert.True(false);
        }
    }
}
