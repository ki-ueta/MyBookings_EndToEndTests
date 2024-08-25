# Getting Started
This repository contains a Playwright.Net test automation framework for MyBookings application.

## Clone the Repository
Bash command
```
git clone https://github.com/ki-ueta/MyBookings_EndToEndTests
```

## Install Dependencies
Bash command
```
cd MyBookings_EndToEndTests
dotnet restore
```

## Configure App Settings
Define the following configuration keys with appropriate values for your application:
 - BaseUrl: The base URL of Mybookings application.
 - Username: A valid username for logging in to Mybookings application.
 - Password: The corresponding password for the username.
 - ClientId: A clientId of Email service
 - ClientSecret: The corresponding clientSecret for the clientId.

## Running Tests
Open a terminal in the project directory.
Run the following command
```
dotnet test
```
