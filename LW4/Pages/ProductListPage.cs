using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LW4.Pages
{
    public class ProductListPage
    {
        private IWebDriver _webDriver;

        #region Locators
        private By _productIdLocator = By.XPath(".//rz-catalog-tile//div[@data-goods-id]");
        #endregion

        #region WebElements
        [FindsBy(How = How.XPath, Using = ".//ul[@class='pagination__list']/li[last()]")]
        public IWebElement LastPage { get; set; }

        [FindsBy(How = How.XPath, Using = ".//ul[@class='pagination__list']/li[1]")]
        public IWebElement FirstPage { get; set; }

        [FindsBy(How = How.XPath, Using = ".//div[contains(@class, 'pagination')]/a[not(contains(@class, 'forward')) and not(contains(@class, 'disabled'))]")]
        public IWebElement PreviousPage { get; set; }

        [FindsBy(How = How.XPath, Using = ".//div[contains(@class, 'pagination')]/a[contains(@class, 'forward') and not(contains(@class, 'disabled'))]")]
        public IWebElement NextPage { get; set; }

        [FindsBy(How = How.XPath, Using = ".//div[contains(@class,'star-inserted')]/h1")]
        public IWebElement ProductListName{ get; set; }

        [FindsBy(How = How.XPath, Using = ".//li[contains(@class, 'pagination__item') and not(contains(./a/text(), '...'))]")]
        public IList<IWebElement> PageNodes { get; set; }

        [FindsBy(How = How.XPath, Using = ".//rz-catalog-tile")]
        public IList<IWebElement> ProductNodes { get; set; }
        #endregion

        public ProductListPage(IWebDriver driver)
        {
            _webDriver = driver;

            PageFactory.InitElements(_webDriver, this);
        }

        public string GetProductUri()
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

        public IWebElement GetProduct(int index)
        {
            if (index >= ProductNodes.Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            return ProductNodes.ElementAt(index);
        }

        public string GetProductId(int index)
        {
            if (index >= ProductNodes.Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            var productNode = ProductNodes.ElementAt(index);

            return GetProductId(productNode);
        }

        public string GetProductId(IWebElement productNode)
        {
            var productIdLocator = By.XPath(".//div[@data-goods-id]");

            if (productNode.FindElements(productIdLocator).Count == 0)
            {
                return null;
            }

            var productId = productNode.FindElement(productIdLocator).GetAttribute("data-goods-id");

            return productId;
        }

        public ProductDetailsPage LoadProduct(int index)
        {
            if (index >= ProductNodes.Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            var productNode = ProductNodes.ElementAt(index);

            return LoadProduct(productNode);
        }

        public ProductDetailsPage LoadProduct(IWebElement productNode)
        {
            var productUriLocator = By.XPath($".//div[@data-goods-id]//a[contains(@class, 'goods-tile__heading')]");

            if (productNode.FindElements(productUriLocator).Count == 0)
            {
                return null;
            }

            var productUri = productNode.FindElement(productUriLocator).GetAttribute("href");

            _webDriver.Navigate().GoToUrl(productUri);

            var productDetailsPage = new ProductDetailsPage(_webDriver);

            return productDetailsPage;
        }

        public bool GoToFirstPage()
        {
            try
            {
                FirstPage.Click();

                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool GoToLastPage()
        {
            try
            {
                LastPage.Click();

                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool GoToPreviousPage()
        {
            try
            {
                PreviousPage.Click();

                return true;
            }
            catch(NoSuchElementException)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool GoToNextPage()
        {
            try
            {
                NextPage.Click();

                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
