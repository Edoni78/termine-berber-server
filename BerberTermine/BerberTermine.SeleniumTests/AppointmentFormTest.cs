using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace BerberTermine.SeleniumTests
{
    public class AppointmentFormTests : IDisposable
    {
        private readonly IWebDriver _driver;

        public AppointmentFormTests()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _driver = new ChromeDriver(options);
        }

        [Fact]
        public void Appointment_Create_ShouldShowSuccessToast()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000");

            var nameInput = _driver.FindElement(By.CssSelector("input[placeholder='Shkruaj emrin']"));
            nameInput.SendKeys("TestUser2");

            var phoneInput = _driver.FindElement(By.CssSelector("input[placeholder='Shkruaj numrin e telefonit']"));
            phoneInput.SendKeys("044123956");

            var dateInput = _driver.FindElement(By.CssSelector("input[placeholder='Kliko për të zgjedhur']"));
            dateInput.Click();
            var today = _driver.FindElement(By.CssSelector(".react-datepicker__day--today"));
            today.Click();

            var timeSelect = _driver.FindElement(By.CssSelector("select.form-select"));
            var options = timeSelect.FindElements(By.TagName("option"));
            options[3].Click();

            var submitBtn = _driver.FindElement(By.CssSelector("button.btn"));
            submitBtn.Click();

            // 🚀 Presim që toast-i të shfaqet
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            var toast = wait.Until(d => d.FindElement(By.CssSelector(".Toastify__toast-body")));

            Assert.Contains("Termini u regjistrua me sukses", toast.Text);
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}