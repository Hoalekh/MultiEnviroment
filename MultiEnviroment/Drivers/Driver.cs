using Microsoft.Playwright;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace MultiEnviroment.Drivers
{
    [Binding]
    public class Driver
    {
        private static IPage _page;
        private static IBrowser _browser;
        private static IBrowserContext _context;
        private static string? baseUrl;

        public IPage Page => _page;

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            baseUrl = TestContext.Parameters.Get("BaseUrl");
            Console.WriteLine(baseUrl);

            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Channel = "chrome",
                SlowMo = 2000
            });
            _context = await _browser.NewContextAsync();

            _page = await _context.NewPageAsync();

            await _page.GotoAsync(baseUrl);
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _browser.CloseAsync();
        }
    }
}
