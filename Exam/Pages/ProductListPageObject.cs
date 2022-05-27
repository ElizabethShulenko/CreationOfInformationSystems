using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Exam.Pages
{
    class ProductListPageObject
    {
        private IWebDriver _webDriver;

        #region Locators

        #endregion

        #region WebElements
        [FindsBy(How = How.XPath, Using = ".//div[contains(@class, 'slider-filter')]/input[@formcontrolname='min']")]
        public IWebElement LowerPrice { get; set; }

        [FindsBy(How = How.XPath, Using = ".//div[contains(@class, 'slider-filter')]/input[@formcontrolname='max']")]
        public IWebElement UpperPrice { get; set; }

        [FindsBy(How = How.XPath, Using = ".//div[contains(@class,'slider-filter')]/button")]
        public IWebElement FilterPriceButton { get; set; }

        [FindsBy(How = How.XPath, Using = ".//rz-catalog-tile")]
        public IList<IWebElement> ProductNodes { get; set; }

        #endregion

        public ProductListPageObject(IWebDriver driver)
        {
            _webDriver = driver;

            PageFactory.InitElements(_webDriver, this);
        }

        public void FilterByPrice(decimal minPrice, decimal maxPrice)
        {
            LowerPrice.Clear();
            LowerPrice.SendKeys(minPrice.ToString());

            UpperPrice.Clear();
            UpperPrice.SendKeys(maxPrice.ToString());

            FilterPriceButton.Click();

            Thread.Sleep(2000);
        }

        public static bool CheckProductsInPriceRange(IEnumerable<IWebElement> products, decimal minPrice, decimal maxPrice)
        {
            var productPriceLocator = By.XPath(".//span[contains(@class,'price-value')]");

            foreach(var product in products)
            {
                var priceString = product.FindElement(productPriceLocator).Text;

                priceString = priceString
                    .Replace(" ", "")
                    .Replace("₴", "");

                var price = Decimal.Parse(priceString);

                if(price < minPrice || price > maxPrice)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
