using NerdStore.BDD.Tests.Config;

namespace NerdStore.BDD.Tests.Usuario
{
    public class CadastroDeUsuarioTela : BaseUsuarioTela
    {
        public CadastroDeUsuarioTela(SeleniumHelper helper) : base(helper)
        { }

        public void ClicarNoLinkRegistrar()
        {
            Helper.ClicarNoLinkPorTexto("Register");
        }

        public void PreencherFormularioRegistro(UsuarioModel usuario)
        {
            Helper.PreencherTextBoxPorId("Input_Email", usuario.Email);
            Helper.PreencherTextBoxPorId("Input_Password", usuario.Senha);
            Helper.PreencherTextBoxPorId("Input_ConfirmPassword", usuario.Senha);
        }
        public bool ValidarPreenchimentoFormularioRegistro(UsuarioModel usuario)
        {
            if (Helper.ObterValorTextBoxPorId("Input_Email") != usuario.Email) return false;
            if (Helper.ObterValorTextBoxPorId("Input_Password") != usuario.Senha) return false;
            if (Helper.ObterValorTextBoxPorId("Input_ConfirmPassword") != usuario.Senha) return false;
            return true;
        }
        public void ClicarNoBotaoRegistrar()
        {
            Helper.ClicarBotaoPorId("registerSubmit");
        }
    }
}
