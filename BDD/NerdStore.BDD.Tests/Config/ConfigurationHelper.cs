using Microsoft.Extensions.Configuration;

namespace NerdStore.BDD.Tests.Config
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _config;
        public string VitrineUrl => _config.GetSection("VitrineUrl").Value;
        public string DomainUrl => _config.GetSection("DomainUrl").Value;
        public string FolderPicture => _config.GetSection("FolderPicture").Value;
        public string ProdutoUrl => $"{DomainUrl}{_config.GetSection("ProdutoUrl").Value}";
        public string CarrinhoUrl => $"{DomainUrl}{_config.GetSection("CarrinhoUrl").Value}";
        public string RegisterUrl => $"{DomainUrl}{_config.GetSection("RegisterUrl").Value}";
        public string LoginUrl => $"{DomainUrl}{_config.GetSection("LoginUrl").Value}";


        public ConfigurationHelper()
        {
            _config = new ConfigurationBuilder()
                            .AddJsonFile($"{AppContext.BaseDirectory}/Config/appsettings.json")
                            .Build();
        }
    }
}
