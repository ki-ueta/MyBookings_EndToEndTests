using Microsoft.Playwright;
using MyBooking_EndToEndTests.Base;
using MyBooking_EndToEndTests.MyBookings.Popups;
using System;
using System.Threading.Tasks;

namespace MyBooking_EndToEndTests.MyBooking.Pages
{
    class CalendarPage: BasePage
    {
        // Elements
        private readonly ILocator _newAppointmentButton;

        // Notification popup
        public readonly ILocator AppointmentSavedNotification;

        // Popup
        public readonly NewAppointmentPopup newAppointmentPopup;

        public CalendarPage(IPage page) : base(page) {

            // Assuming all elements has labels as per its functions

            // Elements
            _newAppointmentButton = _page.GetByLabel("New appointment");
            
            // Notification (alert)
            AppointmentSavedNotification = _page.GetByText("Appointment Saved");

            // Popup
            newAppointmentPopup = new NewAppointmentPopup(_page);
        }

        /*
         * Locators
         */
        public ILocator GetAddedAppointmentLocator(DateTime date, string appointmentName)
        {
            return _page.GetByLabel(date.DayOfWeek.ToString()).Filter(new() { HasText = appointmentName });
        }

        public ILocator GetLoggedInUsernameLocator(string username)
        {
            // Assuming header section has a label "Header"
            return _page.GetByLabel("Header").GetByText(username);
        }
        
        /*
         * Actions
         */
        // Open the new appointment popup, fill in all fields and click save button.
        public async Task AddNewAppointmentAsync(string appointmentName, string date, string time)
        {
            await _newAppointmentButton.ClickAsync();

            // New Appointment popup should be displayed
            await newAppointmentPopup.InputNewAppointmentAsync(appointmentName, date,  time);

        }
    }
}
