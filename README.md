# JokerService

[![C#](https://img.shields.io/badge/C%23-Programming-blueviolet)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Windows Service](https://img.shields.io/badge/Windows_Service-BackgroundService-blue.svg)](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-6.0)
[![YouTube](https://img.shields.io/badge/YouTube-Video-red.svg)](https://www.youtube.com/watch?v=aHC-4ivVDEQ&ab_channel=RobertsDevTalk)
[![.NET 7](https://img.shields.io/badge/.NET-7-512BD4.svg)](https://dotnet.microsoft.com/)
[![Serilog](https://img.shields.io/badge/Serilog-Logging-green.svg)](https://serilog.net/)

This repository contains a .NET Worker Windows Service implementation using `BackgroundService`. The service performs background tasks and demonstrates how to create a long-running service in a Windows environment by logging Computer Programming Jokes.

## Table of Contents

- [Getting Started](#getting-started)
- [Features](#features)
- [Publishing](#publishing)
- [Credits](#credits)

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
  (Creates a `Logs` folder in the project's root directory.)

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
