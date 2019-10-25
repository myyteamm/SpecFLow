using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace SeleniumAndSpecflow
{
    [Binding]
    public class GoogleSearch
    {
        private readonly IWebDriver _webDriver;
        private readonly IWait<IWebDriver> _defaultWait;
        private string _searchTerm;

        public GoogleSearch()
        {
            //string projectpath = PlatformServices.Default.Application.ApplicationBasePath;//AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            _webDriver = new ChromeDriver(@"C:\Users\P10454236\source\repos\SpecFlowTrial\SpecFlowTrial\bin\Debug\netcoreapp2.1\");

            _defaultWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5))
            {
                PollingInterval = TimeSpan.FromMilliseconds(100)
            };
        }

        [Given(@"I navigate to (.*)")]
        public void Navigate(string url)
        {
            if (!url.StartsWith("http") && !url.StartsWith("https"))
                url = "https://" + url;

            _webDriver.Navigate().GoToUrl(url);
            _webDriver.Manage().Window.Maximize();
        }

        [When(@"I search for (.*)")]
        public void Search(string searchTerm)
        {
            _searchTerm = searchTerm;

            IWebElement searchInput = _webDriver.FindElement(By.CssSelector("input[name='q']"));
            searchInput.SendKeys(searchTerm);

            IWebElement searchForm = _webDriver.FindElement(By.CssSelector("form[action=\"/search\"]"));
            searchForm.Submit();
        }

        [Then(@"Google should return valid search results")]
        public void ValidateSearchResults()
        {
            
            var results = _webDriver.FindElement(By.CssSelector("input[name='q']"));
            string pageData = results.GetAttribute("innerText");
           

            Assert.IsNotNull(pageData);

            IWebElement resultsDiv =
                _defaultWait.Until(
                    ExpectedConditions.ElementExists(By.CssSelector($"div[data-async-context=\"query:{_searchTerm}\"]")));

            Assert.IsNotNull(resultsDiv.Text);
        }

        [AfterScenario]
        public void CleanUp()
        {
            _webDriver.Quit();
        }
    }
}
