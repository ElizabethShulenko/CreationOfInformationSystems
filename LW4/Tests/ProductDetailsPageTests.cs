using LW4.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace LW4.Tests
{
    public class ProductDetailsPageTests
    {
        private IWebDriver _webDriver;

        private readonly string _uri = "https://rozetka.com.ua/acer_nh_qbfeu_00a/p282286938/";

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
        public void AddToCart()
        {
            //arrange
            var productDetailsPage = new ProductDetailsPage(_webDriver);

            //act
            var result = productDetailsPage.AddToCart();

            //assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AddToCartMultipleProduct()
        {
            //arrange
            var count = 5;
            var productDetailsPage = new ProductDetailsPage(_webDriver);

            var productPrice = productDetailsPage.GetProductPrice();

            //act
            var result = productDetailsPage.AddToCart(count);

            var sumPriceString = productDetailsPage.CartModalWindowSumPrice.Text;

            sumPriceString = sumPriceString
                .Replace(" ", "")
                .Replace("₴", "");
            
            var sumPrice = Double.Parse(sumPriceString);

            //assert
            Assert.IsTrue(result);
            Assert.AreEqual(productPrice * count, sumPrice);
        }

        [Test]
        public void SortOutProductTabs()
        {
            //arrange
            var productDetailsPage = new ProductDetailsPage(_webDriver);

            //act
            var result = productDetailsPage.SortOutProductTabs();

            //assert
            Assert.IsTrue(result);
        }
    }
}
