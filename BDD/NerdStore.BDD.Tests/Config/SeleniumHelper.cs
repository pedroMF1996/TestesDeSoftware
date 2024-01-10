using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace NerdStore.BDD.Tests.Config
{
    public class SeleniumHelper : IDisposable
    {
        public IWebDriver WebDriver;
        public readonly ConfigurationHelper Configuration;
        public WebDriverWait Wait;

        public SeleniumHelper(Browser browser, ConfigurationHelper configurationHelper, bool headless = false)
        {
            WebDriver = WebDriverFactory.CreateWebDriver(browser, headless);
            Configuration = configurationHelper;
            WebDriver.Manage().Window.Maximize();
            Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30));
        }

        public string ObterUrl()
            => WebDriver.Url;

        public void IrParaUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public void ClicarNoLinkPorTexto(string linkText)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText(linkText))).Click();
        }

        public bool ValidarConteudoUrl(string conteudo)
        {
            return Wait.Until(ExpectedConditions.UrlContains(conteudo));
        }
            
        public void ClicarBotaoPorId(string botaoId)
        {
            var botao = ObterElementoPorId(botaoId);
            botao.Click();
        }

        public void ClicarPorXPath(string xpath)
        {
            var elemento = Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
            elemento.Click();
        }

        public IWebElement ObterElementoPorClasse(string classe)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(classe)));
        }
        
        public IWebElement ObterElementoPorXPath(string xpath)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
        }
        
        public IWebElement ObterElementoPorId(string id)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
        }

        public void PreencherTextBoxPorId(string id, string valorCampo)
        {
            var elemento = ObterElementoPorId(id); 
            elemento.SendKeys(valorCampo);
        }
        
        public void PreencherDropDownPorId(string id, string valorCampo)
        {
            var elemento = new SelectElement(ObterElementoPorId(id)); 
            elemento.SelectByValue(valorCampo);
        }

        public string ObterTextoElementoPorClasseCSS(string classeCss)
        {
            return ObterElementoPorClasse(classeCss).Text;
        }
        
        public string ObterTextoElementoPorId(string id)
        {
            return ObterElementoPorId(id).Text;
        }
        
        public string ObterValorTextBoxPorId(string id)
        {
            return ObterElementoPorId(id).GetAttribute("value");
        }

        public IEnumerable<IWebElement> ObterListaPorClasse(string classe)
        {
            return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName(classe)));
        }

        public bool ValidarSeOElementoExiste(string id)
        {
            return ElementoExistente(By.Id(id));
        }

        public void VoltarNavegacao(int vezes = 1)
        {
            for (int i = 0; i < vezes; i++)
            {
                WebDriver.Navigate().Back();
            }
        }

        public void ObterScreenShot(string nome)
        {
            SalvarScreenShot(WebDriver.TakeScreenshot(), string.Format("(0)_" + nome + ".png", DateTime.Now.ToFileTime()));
        }

        private void SalvarScreenShot(Screenshot screenshot, string fileName)
        {
            screenshot.SaveAsFile($"{Configuration.FolderPicture}/{fileName}");
        }

        private bool ElementoExistente(By by)
        {
            try
            {
                WebDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void Dispose()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }
    }
}
