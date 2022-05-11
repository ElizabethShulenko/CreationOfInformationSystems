using LW4.Models;
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

        [FindsBy(How = How.XPath, Using = ".//input[@search-input]")]
        public IWebElement SearchInput { get; set; }

        [FindsBy(How = How.XPath, Using = ".//button[contains(@class, 'search-form__submit')]")]
        public IWebElement SearchButton { get; set; }
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

        public IEnumerable<Category> GetCategories()
        {
            try
            {
                var result = new List<Category>();

                foreach (var categoryNode in CategoryNodes)
                {
                    result.Add(new Category
                    {
                        Name = categoryNode.Text,
                        Uri = categoryNode.GetAttribute("href")
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public CategoryPage LoadCategory(Category category)
        {
            try
            {
                if (!category.HasUri)
                {
                    return null;
                }

                _webDriver.Navigate().GoToUrl(category.Uri);

                var categoryPage = new CategoryPage(_webDriver);

                return categoryPage;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public SearchPage SearchProducts(string searchText)
        {
            try
            {

                var waiter = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));

                var searchPageLocator = By.XPath(".//div[contains(@class, 'search-header')]/h1");

                SearchInput.SendKeys(searchText);

                SearchButton.Click();

                waiter.Until(ExpectedConditions.ElementIsVisible(searchPageLocator));

                var searchPage = new SearchPage(_webDriver);

                return searchPage;
            }
            catch
            {
                return null;
            }
        }

        public ProductListPage SearchProductList(string searchText)
        {
            try
            {

                var waiter = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));

                var productListPageLocator = By.XPath(".//div[contains(@class,'star-inserted')]/h1");

                SearchInput.SendKeys(searchText);

                SearchButton.Click();

                waiter.Until(ExpectedConditions.ElementIsVisible(productListPageLocator));

                var productListPage = new ProductListPage(_webDriver);

                return productListPage;
            }
            catch
            {
                return null;
            }
        }
    }
}
