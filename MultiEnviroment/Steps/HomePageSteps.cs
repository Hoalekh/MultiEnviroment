using MultiEnviroment.Drivers;
using MultiEnviroment.Page;
using NUnit.Framework;


namespace MultiEnviroment.Steps
{


    [Binding]
    public class HomePageSteps
    {

        public static string InstallationText => "//h1";
        public static string WritingTest => "//a[text()='Writing tests']";
     
        private readonly Driver _driver;
        private readonly HomePage homePage;

        public HomePageSteps(Driver driver)
        {
            _driver = driver;
            homePage = new HomePage(_driver.Page);
        }

        [Given(@"The page displays information ""([^""]*)""")]
        public async Task GivenThePageDisplaysInformation(string installation)
        {
            Assert.IsTrue(await homePage.IsElementVisible(InstallationText));
            Assert.AreEqual(await homePage.GetText(InstallationText), installation);
           
        }

        [Then(@"I click to the writing test")]
        public async Task ThenIClickToTheWritingTest()
        {
            await homePage.ClickToButton(WritingTest);
        }

        



    }
}
