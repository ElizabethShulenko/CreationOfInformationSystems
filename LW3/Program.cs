using System;

namespace LW3
{
    class Program
    {
        static void Main(string[] args)
        {
            var email = "elizavetashulenko";
            var password = "qwerty_123";

            var driver = ChromeDriverUtillity.GetChromeWebDriver("https://accounts.ukr.net/");

            //if (!driver.Login("email", "password"))
            //{
            //    Console.WriteLine("Test Login failed!");
            //    driver.Navigate().Refresh();
            //}

            if (!driver.Login(email, password))
            {
                Console.WriteLine("Login failed!");
                return;
            }
            else
            {
                Console.WriteLine("Login succeed!");
            }


            if (!driver.SendLetter($"{email}@ukr.net", "Hello!", "Hello, world!"))
            {
                Console.WriteLine("Send message failed!");
            }
            else
            {
                Console.WriteLine("Send message succeed!");
            }

            Console.ReadLine();
        }
    }
}
