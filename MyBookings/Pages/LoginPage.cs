using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using MyBooking_EndToEndTests.Base;
using System.Threading.Tasks;

namespace MyBooking_EndToEndTests.MyBooking.Pages
{
    class LoginPage : BasePage
    {
        private readonly ILocator _emailTextbox;
        private readonly ILocator _passwordTextbox;
        private readonly ILocator _loginButton;

        public LoginPage(IPage page) : base(page)
        {
            // Assuming all elements has labels as per its functions
            _emailTextbox = _page.GetByLabel("Email");
            _passwordTextbox= _page.GetByLabel("Password");
            _loginButton = _page.GetByLabel("Log in");
        }

        // Fill in all fields and click Login button
        public async Task LogInAsync(string email, string password)
        {
            await _emailTextbox.FillAsync(email);
            await _passwordTextbox.FillAsync(password);
            await _loginButton.ClickAsync();
        }
    }
}
