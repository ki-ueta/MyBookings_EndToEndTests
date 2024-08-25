using Microsoft.Playwright;
using MyBooking_EndToEndTests.Base;
using System.Threading.Tasks;

namespace MyBooking_EndToEndTests.MyBookings.Popups
{
    class NewAppointmentPopup : BasePage
    {
        private readonly ILocator _appointmentNameTextbox;
        private readonly ILocator _dateTextbox;
        private readonly ILocator _TimeTextbox;
        public readonly ILocator saveButton;

        public NewAppointmentPopup(IPage page) : base(page)
        {
            // Assuming all elements has labels as per its functions

            _appointmentNameTextbox = page.GetByLabel("Appointment name");
            // Assuming date testbox accept date in text format.
            _dateTextbox = page.GetByLabel("Date");
            // Assuming time testbox accept time in text format.
            _TimeTextbox = page.GetByLabel("Time");
            saveButton = page.GetByLabel("Save");
        }

        // Fill in all fields of the new appointment popup 
        public async Task FillInNewAppointment(string appointmentName, string date, string time)
        {
            await _appointmentNameTextbox.FillAsync(appointmentName);
            await _dateTextbox.FillAsync(date);
            await _TimeTextbox.FillAsync(time);
        }

        // Fill in all fields of the new appointment popup and click Save button
        public async Task InputNewAppointmentAsync(string appointmentName, string date, string time)
        {
            await FillInNewAppointment( appointmentName,  date,  time);
            await saveButton.ClickAsync();
        }
    }
}
