using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace NerdStore.BDD.Tests.Config
{
    public class SeleniumHelper : IDisposable
    {
        public IWebDriver WebDriver;
        public readonly ConfigurationHelper ConfigurationHelper;
        public WebDriverWait Wait;

        public SeleniumHelper(Browser browser, bool headless, ConfigurationHelper configurationHelper)
        {
            WebDriver = WebDriverFactory.CreateWebDriver(browser, headless);
            ConfigurationHelper = configurationHelper;
            WebDriver.Manage().Window.Maximize();
            Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30));
        }

        public string ObterUrl()
            => WebDriver.Url;

        public void IrParaUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public void ClicarLinkPorTexto(string linkText)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(linkText))).Click();
        }        

        public void Dispose()
        {
            WebDriver.Dispose();
        }

    }

    public class ConfigurationHelper
    {
        private readonly IConfiguration _config;

        public ConfigurationHelper()
        {
            _config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
        }
    }

    public enum Browser
    {
        Chrome
    }

    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(Browser browser, bool headless)
        {
            IWebDriver driver = null;

            switch (browser)
            {
                case Browser.Chrome:
                    var optionsChrome = new ChromeOptions();
                    
                    if (headless)
                        optionsChrome.AddArgument("--headless");

                    driver = new ChromeDriver(optionsChrome);
                    break;
                default:
                    break;
            }

            return driver;
        }
    }
}
