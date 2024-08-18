# JokerService

[![C#](https://img.shields.io/badge/C%23-Programming-blueviolet)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Windows Service](https://img.shields.io/badge/Windows_Service-BackgroundService-blue.svg)](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-6.0)
[![.NET 8](https://img.shields.io/badge/.NET-8-512BD4.svg)](https://dotnet.microsoft.com/)
[![Serilog](https://img.shields.io/badge/Serilog-Logging-green.svg)](https://serilog.net/)
[![Email Service](https://img.shields.io/badge/Email_Service-SMTP-blue.svg)](https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.smtpclient?view=net-6.0)
[![YouTube](https://img.shields.io/badge/YouTube-Video-red.svg)](https://www.youtube.com/watch?v=aHC-4ivVDEQ&ab_channel=RobertsDevTalk)

This repository contains a .NET Worker Windows Service implementation using `BackgroundService`. The service performs background tasks and demonstrates how to create a long-running service in a Windows environment by logging Computer Programming Jokes.

## Table of Contents

- [Getting Started](#getting-started)
- [Features](#features)
- [Publishing](#publishing)
- [Credits](#credits)
- [Version History](#version-history)

## Getting Started

1. Clone this repository to your local machine.

2. Build the solution using Visual Studio or the command line:

   ```bash
   dotnet build
   ```

3. To run the project in development using Visual Studio, VS Code, or the command line:

   ```bash
   dotnet run
   ```

## Features

- Long-running Windows Service using `BackgroundService`.
- Background tasks and worker logic.
- Logging Computer Programming Jokes with `Serilog` to the current directory.
	- (Creates a `Logs` folder in the project's root directory.)
- Email Service to send the jokes to the specified email address.
	- Email settings are configurable in the `appsettings.json` file.
	- Email settings include the SMTP server, port, email address, and password.
	- Tested with Smtp4Dev. https://github.com/rnwood/smtp4dev
- Timer settings to control the interval of the background tasks.
- Microsoft Teams Channel Integration
- Export text file with jokes

## Publishing

To publish the service as a Windows Service, follow these steps:

1. Open a command prompt or PowerShell with administrator privileges.

2. Navigate to the project folder where the service is located.

3. Use the following command to publish the service:

   ```bash
   dotnet publish -c Release -o ./publish
   ```

   This command creates a 'JokerService.dll' in the `./publish` directory, which acts similarly to 'JokerService.exe' on Windows.

4. Create the Windows Service using the following command:

   ```bash
   sc create JokerService binPath="C:\path\to\your\service\folder\publish\JokerService.dll"
   ```

5. Start the service using:

   ```bash
   sc start JokerService
   ```

   This ensures your service is up and running as a Windows Service.

## Credits

- This project is inspired by the tutorials from:

  [RobertsDevTalk YouTube video](https://www.youtube.com/watch?v=aHC-4ivVDEQ&ab_channel=RobertsDevTalk).

  [Microsoft Learning](https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service?pivots=dotnet-7-0).

## Version History
- Version [1.0.2.0] 
	- Added Export Service to export jokes to a text file.
	- Refactored Worker Service so its' processes and other service calls are split into separate classes.
	___
- Version [1.0.1.2] 
	- Refactored Serilog Configuration to separate class within Settings\ directory. 
	- Renamed SmtpSettings to EmailSettings and made calls configurable
	- Added XML Documentation Comments.	
	___
- Version [1.0.1.1] 
	- Updated Serilog Configuration
	- Added TimersSettings
	- WIP MS Teams Sink Integration
	___
- Version [1.0.1.0] Added Email Service 
- Version [1.0.0.1] Minor CleanUp and Refactoring
- Version [1.0.0.0] Original Release
