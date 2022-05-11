using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace LW4.Pages
{
    public class SearchPage
    {
        private IWebDriver _webDriver;

        #region WebElements
        [FindsBy(How = How.XPath, Using = ".//input[@search-input]")]
        public IWebElement SearchInput { get; set; }

        [FindsBy(How = How.XPath, Using = ".//button[contains(@class,'search-form__submit')]")]
        public IWebElement SearchButton { get; set; }

        [FindsBy(How = How.XPath, Using = ".//div[contains(@class, 'search-header')]/h1")]
        public IWebElement SearchResult { get; set; }
        #endregion

        public SearchPage(IWebDriver driver)
        {
            _webDriver = driver;

            PageFactory.InitElements(_webDriver, this);
        }
    }
}
