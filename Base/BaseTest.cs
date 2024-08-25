using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.Testing.Platform.Configurations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyBooking_EndToEndTests.Base
{

    [TestClass]
    public class BaseTest: PageTest
    {
        public readonly IBrowser _browser;
        public readonly IPage _page;
        public readonly IConfiguration _configuration;

        public BaseTest(BrowserFactory browserFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _browser = browserFactory.CreateBrowserAsync().Result;
            _page = _browser.NewPageAsync().Result;
            _page.GotoAsync(configuration["url"]);
        }

        [ClassInitialize]
        public static void GlobalSetup(TestContext context)
        {
            SetDefaultExpectTimeout(10_000);
        }

    }
}
