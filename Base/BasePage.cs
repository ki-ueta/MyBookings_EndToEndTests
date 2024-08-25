using Microsoft.Playwright;

namespace MyBooking_EndToEndTests.Base
{
    partial class BasePage
    {
        protected readonly IPage _page;

        public BasePage(IPage page)
        {
            _page = page;
        }

        
    }
}
