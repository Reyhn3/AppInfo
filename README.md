# AppInfo

Outputs information about the application and its conditions during startup.

Have you ever received a bug without knowing what version of the application was running, or what environment it was running in?

Then this library can help you.

This library provides a simple and customizable way to output information about the application and its conditions during startup. A simple set of text-based information that can be easily copy-pasted into bug reports, used by sysadmins and devops teams, confirm deployments, troubleshoot environmental behavior and much more.


# Usage

Add the package to the application project.

Create an AppInfo in the `Main` method:

```csharp
using AppInfo;

AppInfoBuilder
	.CreateDefaultBuilder()
	.Build();
```

Ideally, this should be done at or near the top of the application entry point; as early as possible in the application startup.

Optionally configure the `IAppInfo` before building it.

When starting the application, this library will output detailed information about the hosting application:

```
Application created with context:
  Product:     AppInfo Demo - Basic
  Version:     1.0.0+e112654fd1f985e1f7cb11e0ab76165b54a7e590
  Assembly:    basic
  File Name:   basic.dll
  Is Release:  false
  64-bit:      true
  Location:    /home/Reyhn3/Projects/AppInfo/examples/Basic/bin/Debug/net8.0
  Base:        /home/Reyhn3/Projects/AppInfo/examples/Basic/bin/Debug/net8.0/
  Environment: Production
  MachineName: devmachine
  OSVersion:   Unix 7.1.8.200
  ClrVersion:  8.0.29
  ProcessId:   2536696
```


# Configuration

The [examples](https://github.com/Reyhn3/AppInfo/tree/main/examples) folder contains runnable demo projects for different types of host applications and usages.


# Features

* **Text first**
<br/>All information is key-value pairs that are rendered as text that can easily be copied.

* **Non-invasive**
<br/>This library is designed to be as unobtrusive as possible, while still providing a wealth of information about the application and its running conditions.

* **Fail-safe**
<br/>All operations (information extraction, assembly and outputting) are wrapped in `try-catch` to ensure this library can never break your application.

* **Customizable**
<br/> Add custom information to the output.

* **Extensible**
<br/> Extend the library with custom information providers.

* **Configurable**
<br/> Configure where and how to output the information.
