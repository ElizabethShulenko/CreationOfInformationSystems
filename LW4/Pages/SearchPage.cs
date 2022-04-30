using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace LW4.Pages
{
    public class SearchPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.XPath, Using = ".//input[@search-input]")]
        public IWebElement SearchInput { get; set; }

        [FindsBy(How = How.XPath, Using = ".//button[contains(@class,'search-form__submit')]")]
        public IWebElement SearchFindButton { get; set; }
    }
}
