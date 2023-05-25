
using Microsoft.Playwright;

namespace MultiEnviroment.Page
{
    public class HomePage
    {

        private readonly IPage page;

        public HomePage(IPage page) => this.page = page;
   

        public IPage GetPage() => this.page;
        public async Task NavigatePage(string url)
        {
            await this.GetPage().GotoAsync(url);

        }

        public async Task ClickToButton(string locator)
        {
            await this.GetPage().Locator(locator).ClickAsync();

        }
      

        public async Task FillTextBox(string locator, string value)
        {
            try
            {
                await this.GetPage().Locator(locator).FillAsync(value);
            }
            catch (Exception ex)
            {
                throw new Exception($"your message here: {ex.Message}");
            }

        }
        public async Task<string> GetText(string locator)
        {
            try
            {
                var element = await this.GetPage().QuerySelectorAsync(locator);
                var text = await element!.TextContentAsync();
                return text!.TrimEnd();
            }
            catch (Exception ex)
            {
                throw new Exception($"your message here: {ex.Message}");

            }
        }

        public async Task<bool> IsElementVisible(string locator)
        {
            try
            {
                await GetPage().WaitForSelectorAsync(locator);
                var element = await GetPage().QuerySelectorAsync(locator);
                if (element == null)
                {
                    throw new Exception($"Element with locator '{locator}' not found.");
                }
                return await element.IsVisibleAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"your message here: {ex.Message}");
            }

        }
        public async Task HoverToElement(string locator)
        {
            await GetPage().WaitForSelectorAsync(locator);
            var element = await GetPage().QuerySelectorAsync(locator);
            if (element == null)
            {
                throw new Exception($"Element with locator '{locator}' not found.");
            }
            await element.HoverAsync();
        }
        public async Task<List<string?>> GetDropdownOptions(string locator)
        {
            await GetPage().WaitForSelectorAsync(locator);
            var dropdownElements = await GetPage().QuerySelectorAllAsync(locator);
            var optionTextTasks = dropdownElements.Select(async element => await element.TextContentAsync());
            var optionTexts = await Task.WhenAll(optionTextTasks);

            return optionTexts.ToList();
        }

   



    }

}