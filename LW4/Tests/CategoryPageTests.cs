using LW4.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;

namespace LW4.Tests
{
    class CategoryPageTests
    {
        private IWebDriver _webDriver;

        private readonly string _uri = "https://rozetka.com.ua/computers-notebooks/c80253/";

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
        public void LoadProductsList()
        {
            //arrange
            var categoryPage = new CategoryPage(_webDriver);

            var productLists = categoryPage.GetProductLists();
            var productListsCount = categoryPage.ProductListNodes.Count;

            var productList = productLists.First();

            //act
            var productListPage = categoryPage.LoadProductList(productList);
            var productListPageUri = productListPage.GetProductUri();

            //assert
            Assert.AreEqual(productLists.Count(), productListsCount);
            Assert.AreEqual(productList.Uri, productListPageUri);
            Assert.NotNull(productListPage);
        }
    }
}
