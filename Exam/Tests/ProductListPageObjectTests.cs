using Exam.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace Exam.Tests
{
    [TestFixture]
    public class ProductListPageObjectTests
    {
        private IWebDriver _webDriver;

        private readonly string _uri = "https://rozetka.com.ua/notebooks/c80004/";

        #region Startup
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _webDriver = new ChromeDriver();

            _webDriver.Manage().Window.Maximize();
        }

        [SetUp]
        public void SetUp()
        {
            _webDriver.Navigate().GoToUrl(_uri);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _webDriver.Quit();
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Manage().Cookies.DeleteAllCookies();
        }
        #endregion

        [Test]
        public void GetProducts()
        {
            //arrange
            var productListPageObject = new ProductListPageObject(_webDriver);

            var minPrice = 3000;
            var maxPrice = 20000;

            //act
            productListPageObject.FilterByPrice(minPrice, maxPrice);

            var products = productListPageObject.ProductNodes;

            var result = ProductListPageObject.CheckProductsInPriceRange(products, minPrice, maxPrice);

            //assert
            Assert.IsTrue(result);
        }
    }
}
