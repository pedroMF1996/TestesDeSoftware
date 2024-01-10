using NerdStore.BDD.Tests.Config;

namespace NerdStore.BDD.Tests.Usuario
{
    public abstract class BaseUsuarioTela : PageObjectModel
    {
        public BaseUsuarioTela(SeleniumHelper helper) : base(helper)
        {
        }

        public void AcessarSiteLoja()
        {
            Helper.IrParaUrl(Helper.Configuration.DomainUrl);
        }

        public bool ValidarSaudacaoUsuario(UsuarioModel usuario)
        {
            if (!Helper.ValidarSeOElementoExiste("saudacaoUsuario"))
                return false;

           return Helper.ObterTextoElementoPorId("saudacaoUsuario").Contains(usuario.Email);
        }

        public bool ValidarMensagemDeErro(string erroMsg)
        {
            return Helper.ObterTextoElementoPorClasseCSS("validation-summary-errors").Contains(erroMsg);
        }
    }
}
