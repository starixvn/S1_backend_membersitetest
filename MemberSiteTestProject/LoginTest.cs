using Microsoft.Playwright;

namespace autotest
{
    [TestFixture]
    public class AutoTestSample
    {
        [Test]
        public void LoginTestMethod()
        {
            Assert.IsTrue(LoginTest().Result.Contains("EttaTestMYR1"));
        }

        private async Task<string> LoginTest()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions { Headless = false });
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://stage.uatasia.com/en");
            await page.GetByPlaceholder("Username").ClickAsync();
            await page.GetByPlaceholder("Username").FillAsync("EttaTestMYR1");
            await page.GetByPlaceholder("Password").ClickAsync();
            await page.GetByPlaceholder("Password").FillAsync("1qaz@WSX");
            await page.GetByRole(AriaRole.Button, new() { Name = "Log In" }).ClickAsync();
            await Task.Delay(5000);
            var result = await page.TextContentAsync("text=Hello,");

            return result;
        }
    }
}
