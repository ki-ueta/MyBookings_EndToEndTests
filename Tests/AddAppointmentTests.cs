using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Testing.Platform.Configurations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyBooking_EndToEndTests.Base;
using MyBooking_EndToEndTests.Helpers;
using MyBooking_EndToEndTests.MyBooking.Pages;

namespace MyBooking_EndToEndTests.Tests;


[TestClass]
public class AddAppointmentTests : BaseTest
{
    private readonly DateTime _tomorrow;
    private CalendarPage _calendarPage;

    public AddAppointmentTests(BrowserFactory browserFactory, IConfiguration configuration) :base(browserFactory, configuration)
    {
    }

    [TestInitialize()]
    public async Task StartupAsync()
    {
        // Log in
        var loginPage = new LoginPage(_page);
        await loginPage.LogInAsync(_configuration["MyBookings:username"], _configuration["MyBookings:password"]);

        // If login successfully, the username should be displayed on the top right corner of the page.
        _calendarPage = new CalendarPage(_page);
        await Expect(_calendarPage.GetLoggedInUsernameLocator(_configuration["MyBookings:username"])).ToBeVisibleAsync();
    }

    [TestMethod]
    [DataRow("meeting on the same day",0, DisplayName ="New appointment can be created on the same day")]
    [DataRow("meeting in the future",7, DisplayName = "New appointment can be created on the day in the future")]
    public async Task AppointmentCanBeAdded(string appointmentName, int numberOfDays)
    {
        // Set the input value
        var appointmentDate = DateTime.Now.AddDays(numberOfDays);
        var appointmentDateString = appointmentDate.ToShortDateString();
        var appointmentTimeString = $"{appointmentDate.AddMinutes(5).ToShortTimeString()}-{appointmentDate.AddHours(1).ToShortTimeString()}";

        // Add a new appointment
        await _calendarPage.AddNewAppointmentAsync(appointmentName, appointmentDateString, appointmentTimeString);

        // Assert the appointment saved notification displayed
        await Expect(_calendarPage.AppointmentSavedNotification).ToBeVisibleAsync();
        // Assert the new appointment is visible and has the entered details
        await Expect(_calendarPage.GetAddedAppointmentLocator(appointmentDate,appointmentName)).ToBeVisibleAsync();        
        // Assert email notification is sent out to the user (Assuming subject name as per the code)
        var receivedEmail = await CheckEmailHelper.CheckUnreadEmailsWithSubjectAsync($"New appointment \"{appointmentName}\" has been created on {appointmentDateString} {appointmentTimeString}", _configuration);
        receivedEmail.Should().NotBeNull();
    }

    [TestMethod]
    public async Task AppointmentCannotBeAddedWhenAllRequiredFieldsAreNotEntered(string appointmentName)
    {
        // Set the input value
        var appointmentDate = DateTime.Now.AddDays(1);
        var appointmentDateString = appointmentDate.ToShortDateString();

        // Add a new appointment
        await _calendarPage.AddNewAppointmentAsync("Incomplete meeting", appointmentDateString, "");

        // Assert save button is disabled when all required fields are not entered (Assuming Appointment name, date and times are required fields)
        await Expect(_calendarPage.newAppointmentPopup.saveButton).ToBeDisabledAsync();
    }
}