using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace LW4.Pages
{
    public class LoginPage
    {
        private IWebDriver _webDriver;

        private By _loginWarningLocator = By.XPath(".//div[contains(@class,'type_warning')]");
        private By _userProfileMenuLocator = By.XPath(".//ul[contains(@class,'auth__links')]");

        [FindsBy(How = How.XPath, Using = ".//input[@type='email']")]
        public IWebElement UserEmailInput { get; set; }

        [FindsBy(How = How.XPath, Using = ".//input[@type='password']")]
        public IWebElement UserPasswordInput { get; set; }

        [FindsBy(How = How.XPath, Using = ".//button[contains(@class,'auth-modal__submit')]")]
        public IWebElement LoginSubmitButton { get; set; }

        public LoginPage(IWebDriver driver)
        {
            _webDriver = driver;

            PageFactory.InitElements(_webDriver, this);
        }

        public bool Login(string email, string password)
        {
            try
            {
                var waiter = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(120));

                UserEmailInput.Clear();
                UserEmailInput.SendKeys(email);

                UserPasswordInput.Clear();
                UserPasswordInput.SendKeys(password);

                LoginSubmitButton.Click();

                waiter.Until(d => d.FindElements(_loginWarningLocator).Count > 0 || d.FindElements(_userProfileMenuLocator).Count > 0);

                return _webDriver.FindElements(_userProfileMenuLocator).Count > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
