using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace LW4.Pages
{
    public class ProductDetailsPage
    {
        private IWebDriver _webDriver;

        #region Locators
        private By _mainTabLocator = By.XPath(".//product-tab-main");
        private By _specificationsTabLocator = By.XPath(".//product-tab-characteristics");
        private By _reviewTabLocator = By.XPath(".//product-tab-feedback");
        private By _qaTabLocator = By.XPath(".//product-tab-buyers-questions");
        private By _videosTabLocator = By.XPath(".//app-product-tab-video");
        private By _photosTabLocator = By.XPath(".//product-tab-photo");
        private By _accessoriesTabLocator = By.XPath(".//product-tab-accessories");

        private By _productInCartLocator = By.XPath(".//button[contains(@class, 'buy-button_state_in-cart')]");
        private By _cartLocator = By.XPath(".//div[contains(@class, 'modal__holder') and contains(@class, 'show')]");
        private By _addToCartLocator = By.XPath(".//div[contains(@class, 'product-trade')]//button[contains(@class, 'buy-button') and contains(@class, 'ng-star-inserted')]");
        #endregion

        #region WebElements
        [FindsBy(How = How.XPath, Using = ".//div[contains(@class, 'modal__holder')]//input[@data-testid='cart-counter-input']")]
        public IWebElement CartModalWindowInputCounter { get; set; }

        [FindsBy(How = How.XPath, Using = ".//div[contains(@class, 'sum-price')]")]
        public IWebElement CartModalWindowSumPrice { get; set; }

        [FindsBy(How = How.XPath, Using = ".//p[contains(@class, 'product-prices__big')]")]
        public IWebElement ProductPrice { get; set; }

        [FindsBy(How = How.XPath, Using = ".//p[contains(@class, 'product__code')]")]
        public IWebElement ProductId { get; set; }

        [FindsBy(How = How.XPath, Using = ".//ul[@class='tabs__list']/li")]
        public IList<IWebElement> ProductTabs { get; set; }
        #endregion

        public ProductDetailsPage(IWebDriver driver)
        {
            _webDriver = driver;

            PageFactory.InitElements(_webDriver, this);
        }

        public bool SortOutProductTabs()
        {
            try
            {
                var waiter = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));

                foreach (var productTab in ProductTabs)
                {
                    productTab.Click();

                    waiter.Until(d => d.FindElements(_mainTabLocator).Count > 0
                        || d.FindElements(_specificationsTabLocator).Count > 0
                        || d.FindElements(_reviewTabLocator).Count > 0
                        || d.FindElements(_qaTabLocator).Count > 0
                        || d.FindElements(_videosTabLocator).Count > 0
                        || d.FindElements(_photosTabLocator).Count > 0
                        || d.FindElements(_accessoriesTabLocator).Count > 0);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddToCart()
        {
            try
            {
                var waiter = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(60));

                var addToCartButton = waiter.Until(ExpectedConditions.ElementIsVisible(_addToCartLocator));

                addToCartButton.Click();

                waiter.Until(ExpectedConditions.ElementIsVisible(_productInCartLocator));

                var result = _webDriver.FindElements(_productInCartLocator).Count > 0;

                return result;
            }
            catch
            {
                return false;
            }
        }

        public bool AddToCart(int count)
        {
            if (!AddToCart())
            {
                return false;
            }

            try
            {
                var waiter = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(60));

                waiter.Until(ExpectedConditions.ElementExists(_cartLocator));

                CartModalWindowInputCounter.Clear();
                CartModalWindowInputCounter.SendKeys(count.ToString());

                waiter.Until(ExpectedConditions.ElementIsVisible(_cartLocator));

                Thread.Sleep(1000);

                waiter.Until(ExpectedConditions.ElementToBeClickable(CartModalWindowSumPrice));

                return true;
            }
            catch
            {
                return false;
            }
        }

        public double GetProductPrice()
        {
            try
            {
                var waiter = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(60));

                waiter.Until(ExpectedConditions.ElementToBeClickable(ProductPrice));

                var priceString = ProductPrice.Text;

                priceString = priceString
                    .Replace(" ", "")
                    .Replace("₴", "");

                return Double.Parse(priceString);
            }
            catch
            {
                return 0;
            }
        }

        public string GetProductId()
        {
            try
            {
                var productIdRegex = new Regex(@"\d+", RegexOptions.Compiled);

                var productIdString = ProductId.Text;

                return productIdRegex.Match(productIdString).Value;
            }
            catch
            {
                return null;
            }
        }
    }
}
