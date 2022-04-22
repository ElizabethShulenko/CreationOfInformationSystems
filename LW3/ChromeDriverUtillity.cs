using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace LW3
{
    public static class ChromeDriverUtillity
    {
        public static IWebDriver GetChromeWebDriver(string url)
        {
            try
            {
                var driver = new ChromeDriver();

                driver.Navigate().GoToUrl(url);

                return driver;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return null;
            }
        }

        public static bool Login(this IWebDriver driver, string email, string password)
        {
            try
            {
                var waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                driver.FindElement(By.XPath(".//input[@type='text']")).SendKeys(email);

                driver.FindElement(By.XPath(".//input[@type='password']")).SendKeys(password);

                var submitButton = driver.FindElement(By.XPath(".//button[@type='submit']"));

                submitButton.Click();

                waiter.Until(d => d.FindElements(By.XPath(".//aside[@class='sidebar']")).Count > 0
                    || d.FindElements(By.XPath(".//form/p[contains(./text(), 'Неправильні дані')]")).Count > 0);

                return driver.FindElements(By.XPath(".//aside[@class='sidebar']")).Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return false;
            }
        }

        public static bool SendLetter(this IWebDriver driver, string recipient, string subject, string letter)
        {
            try
            {
                driver.Navigate().GoToUrl("https://mail.ukr.net/desktop#sendmsg");

                var waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                driver.FindElement(By.XPath(".//input[@name='toFieldInput']")).SendKeys(recipient);

                driver.FindElement(By.XPath(".//input[@name='subject']")).SendKeys(subject);

                driver.FindElement(By.XPath("..//body[@id='tinymce']")).SendKeys(letter);

                var submitButton = driver.FindElement(By.XPath(".//div[contains(@class, 'sendmsg__bottom')]/button[contains(@class, 'send')]"));

                submitButton.Click();

                waiter.Until(d => d.FindElements(By.XPath(".//div[@class='sendmsg__ads-ready']")).Count > 0);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return false;
            }
        }
    }
}
