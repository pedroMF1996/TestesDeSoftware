using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NerdStore.BDD.Tests.Config
{
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
