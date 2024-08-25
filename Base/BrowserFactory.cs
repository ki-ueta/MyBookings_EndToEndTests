using Microsoft.Playwright;
using Microsoft.Testing.Platform.Configurations;
using System;
using System.Threading.Tasks;

namespace MyBooking_EndToEndTests.Base
{
    public class BrowserFactory
    {
        private readonly IConfiguration _configuration;

        public BrowserFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IBrowser> CreateBrowserAsync()
        {
            var browserType = _configuration["browserType"];

            using var playwright = await Playwright.CreateAsync();
            switch (browserType)
            {
                case "chromium":
                    return await playwright.Chromium.LaunchAsync();
                case "firefox":
                    return await playwright.Firefox.LaunchAsync();
                case "webkit":
                    return await playwright.Webkit.LaunchAsync();
                default:
                    throw new ArgumentException($"Invalid browser type: {browserType}");
            }
        }
    }
}
