using Microsoft.Playwright;
using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace MultiEnviroment.Drivers
{
    [TestFixture]
    [Binding]
    public class Driver
    {
        private static IPage? _page;
        private static IBrowser? _browser;
        private static IBrowserContext? _context;
        private static string? baseUrl;

        public IPage Page => _page!;

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            string? currentDirectory = Directory.GetCurrentDirectory();
            string? parentDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent?.FullName;
            string jsonPath = Path.Combine(parentDirectory!, "Drivers", "specflow.json");


            if (File.Exists(jsonPath))
            {
                string json = File.ReadAllText(jsonPath);
                dynamic config = JsonConvert.DeserializeObject(json);
                foreach (dynamic env in config.environments)
                {
                    var playwright = await Playwright.CreateAsync();
                    _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = false,
                        Channel = "chrome",
                        SlowMo = 2000
                    });
                    _context = await _browser.NewContextAsync();

                    _page = await _context.NewPageAsync();

                    baseUrl = env.baseUrl;
                    await _page.GotoAsync(baseUrl);

                }
            }
            else
            {
                throw new FileNotFoundException($"The JSON file '{jsonPath}' does not exist.");
            }
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            if (_page != null)
            {
                await _page.CloseAsync();
            }
            if (_browser != null)
            {
                await _browser.CloseAsync();
            }
        }
    }
}
