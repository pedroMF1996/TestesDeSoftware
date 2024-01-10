using NerdStore.BDD.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.BDD.Tests.Usuario
{
    public class LoginDeUsuarioTela : BaseUsuarioTela
    {
        public LoginDeUsuarioTela(SeleniumHelper helper) : base(helper)
        {
        }

        public void ClicarNoLogin()
        {
            Helper.ClicarNoLinkPorTexto("Login");
        }

        public void PreencherUsuarioESenha(UsuarioModel usuario)
        {
            Helper.PreencherTextBoxPorId("Input_Email", usuario.Email);
            Helper.PreencherTextBoxPorId("Input_Password", usuario.Senha);
        }

        public bool ValidarPreenchimentoFormularioLogin(UsuarioModel usuario)
        {
            if (Helper.ObterValorTextBoxPorId("Input_Email") != usuario.Email) return false;
            if (Helper.ObterValorTextBoxPorId("Input_Password") != usuario.Senha) return false;
            return true;
        }

        public void ClicarNoBotaoDeLogin()
        {
            Helper.ClicarBotaoPorId("login-submit");
        }

        public bool Login(UsuarioModel usuario)
        {
            AcessarSiteLoja();
            ClicarNoLogin();
            PreencherUsuarioESenha(usuario);
            if(!ValidarPreenchimentoFormularioLogin(usuario)) return false;
            ClicarNoBotaoDeLogin();
            if(!ValidarSaudacaoUsuario(usuario)) return false;

            return true;
        }
    }
}
