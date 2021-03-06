using LW4.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;

namespace LW4.Tests
{
    [TestFixture]
    public class HomePageTests
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
        public void Login_Success()
        {
            //arrange
            var homePage = new HomePage(_webDriver);

            //act
            var result = homePage.Login("email@gmail.com", "password");

            //assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Login_Failed()
        {
            //arrange
            var homePage = new HomePage(_webDriver);

            //act
            var result = homePage.Login("test", "test");

            //assert
            Assert.IsFalse(result);
        }

        [Test]
        public void LoadCategories()
        {
            //arrange
            var homePage = new HomePage(_webDriver);

            var categories = homePage.GetCategories();
            var categoryNodesCount = homePage.CategoryNodes.Count;

            var category = categories.First();

            //act
            var categoryPage = homePage.LoadCategory(category);
            var categoryPageUri = categoryPage.GetCategoryUri();

            //assert
            Assert.AreEqual(categories.Count(), categoryNodesCount);
            Assert.AreEqual(category.Uri, categoryPageUri);
            Assert.NotNull(categoryPage);
        }
    }
}
