using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace LW4.Pages
{
    public class HomePage
    {
        private IWebDriver _webDriver;

        #region Locators
        private By _loginModalWindowLocator = By.XPath(".//div[contains(@class, 'modal__holder')]");
        #endregion

        #region WebElements
        [FindsBy(How = How.XPath, Using = ".//ul[contains(@class, 'menu-categories') and contains(@class, 'main')]//a[not(contains(@class, 'bordered'))]")]
        public IList<IWebElement> CategoryNodes { get; set; }

        [FindsBy(How = How.XPath, Using = ".//button[contains(@class, 'main-auth__button')]")]
        public IWebElement LoginShowButton { get; set; }
        #endregion

        public HomePage(IWebDriver driver)
        {
            _webDriver = driver;

            PageFactory.InitElements(_webDriver, this);
        }

        public bool Login(string email, string password)
        {
            try
            {
                var waiter = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(60));

                LoginShowButton.Click();

                waiter.Until(ExpectedConditions.ElementExists(_loginModalWindowLocator));

                var loginPage = new LoginPage(_webDriver);

                var result = loginPage.Login(email, password);

                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}
