using LW4.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            var result = homePage.Login("liza.shulenko@gmail.com", "yelizavetaSH21");

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
        public void OpenCategories()
        {
            //arrange
            var homePage = new HomePage(_webDriver);

            //act

            //assert
        }
    }
}
