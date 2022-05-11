using LW4.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;

namespace LW4.Tests
{
    [TestFixture]
    public class SearchPageTests
    {
        private IWebDriver _webDriver;

        private readonly string _uri = "https://rozetka.com.ua/";

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
            _webDriver.Url = _uri;
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
        public void SearchProducts()
        {
            //arrange
            var searchText = "Автоматы";

            var homePage = new HomePage(_webDriver);

            //act
            var searchPage = homePage.SearchProducts(searchText);

            //assert
            Assert.AreEqual($"«{searchText}»", searchPage.SearchResult.Text);
            Assert.NotNull(searchPage);
        }

        [Test]
        public void SearchProductList()
        {
            //arrange
            var searchText = "Миксеры";

            var homePage = new HomePage(_webDriver);

            //act
            var productListPage = homePage.SearchProductList(searchText);

            //assert
            Assert.AreEqual(searchText, productListPage.ProductListName.Text);
            Assert.NotNull(productListPage);
        }

        [Test]
        public void SearchProduct_Failed()
        {
            //arrange
            var searchText = "АвтоматыМиксер";

            var homePage = new HomePage(_webDriver);

            //act
            var productListPage = homePage.SearchProductList(searchText);
            var searchPage = homePage.SearchProducts(searchText);

            //assert
            Assert.IsNull(productListPage);
            Assert.IsNull(searchPage);
        }
    }
}
