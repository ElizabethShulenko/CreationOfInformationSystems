using LW4.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace LW4.Tests
{
    class ProductListPageTests
    {
        private IWebDriver _webDriver;

        private readonly string _uri = "https://rozetka.com.ua/notebooks/c80004/";

        private readonly Random _random = new Random();

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
        public void GetProductId()
        {
            //arrange
            var productListPage = new ProductListPage(_webDriver);

            var productsCount = productListPage.ProductNodes.Count;
            var productIndex = _random.Next(productsCount - 1);

            //act
            var productNode = productListPage.GetProduct(productIndex);
            var productNodeId = productListPage.GetProductId(productNode);

            var productId = productListPage.GetProductId(productIndex);

            var productDetailsPage = productListPage.LoadProduct(productNode);
            var productDetailsPageId = productDetailsPage.GetProductId();

            //assert
            Assert.NotNull(productNodeId);
            Assert.NotNull(productId);

            Assert.IsNotEmpty(productNodeId);
            Assert.IsNotEmpty(productId);

            Assert.AreEqual(productNodeId, productDetailsPageId);
            Assert.AreEqual(productId, productDetailsPageId);
        }

        [Test]
        public void LoadProductByIndex()
        {
            //arrange
            var productListPage = new ProductListPage(_webDriver);

            var productsCount = productListPage.ProductNodes.Count;
            var productIndex = _random.Next(productsCount - 1);

            //act
            var productDetailsPage = productListPage.LoadProduct(productIndex);
            var productDetailsPageId = productDetailsPage.GetProductId();

            //assert
            Assert.NotNull(productDetailsPageId);
        }

        [Test]
        public void LoadProductByWebElement()
        {
            //arrange
            var productListPage = new ProductListPage(_webDriver);

            var productsCount = productListPage.ProductNodes.Count;
            var productIndex = _random.Next(productsCount - 1);

            //act
            var productNode = productListPage.GetProduct(productIndex);

            var productDetailsPage = productListPage.LoadProduct(productNode);
            var productDetailsPageId = productDetailsPage.GetProductId();

            //assert
            Assert.NotNull(productDetailsPageId);
        }

        [Test]
        public void GoToPreviousPage()
        {
            //arrange
            var productListPage = new ProductListPage(_webDriver);

            //act
            if(productListPage.PageNodes.Count == 1)
            {
                return;
            }

            productListPage.GoToFirstPage();
            var failed = productListPage.GoToPreviousPage();

            productListPage.GoToLastPage();
            var success = productListPage.GoToPreviousPage();
            
            //assert
            Assert.IsFalse(failed);
            Assert.IsTrue(success);
        }

        [Test]
        public void GoToNextPage()
        {
            //arrange
            var productListPage = new ProductListPage(_webDriver);

            //act
            if (productListPage.PageNodes.Count == 1)
            {
                return;
            }

            productListPage.GoToFirstPage();
            var success = productListPage.GoToNextPage();
            
            productListPage.GoToLastPage();
            var failed = productListPage.GoToNextPage();

            //assert
            Assert.IsFalse(failed);
            Assert.IsTrue(success);
        }
    }
}
