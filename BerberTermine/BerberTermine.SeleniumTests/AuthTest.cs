using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace BerberTermine.SeleniumTests
{
    public class AuthTest : IDisposable
    {
        private readonly IWebDriver _driver;

        public AuthTest()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _driver = new ChromeDriver(options);
        }

        [Fact]
        public void Login_WithValidCredentials_ShouldRedirectToDashboard()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/login");

            // 1. Plotëso Username
            var usernameInput = _driver.FindElement(By.CssSelector("input.form-control"));
            usernameInput.SendKeys("Adhurim");

            // 2. Plotëso Password
            var passwordInput = _driver.FindElements(By.CssSelector("input.form-control"))[1];
            passwordInput.SendKeys("admin2025");

            // 3. Kliko butonin Kyçu
            var submitBtn = _driver.FindElement(By.CssSelector("button.btn-dark"));
            submitBtn.Click();

            // 4. Prit derisa URL të bëhet /dashboard
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.Url.Contains("/dashboard"));

            // 5. Verifiko që jemi në dashboard
            Assert.Contains("/dashboard", _driver.Url);
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}