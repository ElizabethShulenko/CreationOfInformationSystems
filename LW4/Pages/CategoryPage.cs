using LW4.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace LW4.Pages
{
    public class CategoryPage
    {
        private IWebDriver _webDriver;

        #region Locators
        private By _productListUriLocator = By.XPath(".//a[@class='tile-cats__picture']");
        #endregion

        #region WebElements
        [FindsBy(How = How.XPath, Using = ".//h1[contains(@class, 'portal__heading')]")]
        public IWebElement CategoryName { get; set; }

        [FindsBy(How = How.XPath, Using = ".//rz-list-tile//div")]
        public IList<IWebElement> ProductListNodes { get; set; }
        #endregion

        public CategoryPage(IWebDriver driver)
        {
            _webDriver = driver;

            PageFactory.InitElements(_webDriver, this);
        }

        public IEnumerable<Category> GetProductLists()
        {
            try
            {
                var result = new List<Category>();

                foreach (var productListNodes in ProductListNodes)
                {
                    result.Add(new Category
                    {
                        Name = productListNodes.Text,
                        Uri = productListNodes.FindElement(_productListUriLocator).GetAttribute("href")
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ProductListPage LoadProductList(Category category)
        {
            try
            {
                if (!category.HasUri)
                {
                    return null;
                }

                _webDriver.Navigate().GoToUrl(category.Uri);

                var productListPage = new ProductListPage(_webDriver);

                return productListPage;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GetCategoryUri()
        {
            try
            {
                var result = _webDriver.Url;

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
