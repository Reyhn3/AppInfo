# AppInfo

Outputs information about the application and its conditions during startup.

Have you ever received a bug without knowing what version of the application was running, or what environment it was running in?

Then this library can help you.

This library provides a simple and customizable way to output information about the application and its conditions during startup. A simple set of text-based information that can be easily copy-pasted into bug reports, used by sysadmins and devops teams, confirm deployments, troubleshoot environmental behavior and much more.


# Usage

Add the package to the application project.

Create an AppInfo in the `Main` method:

```csharp
using AppInformation;

AppInfo.BuildAndOutputDefault();
```

> [!TIP]
> Ideally, this should be done **at or near** the top of the application entry point; as early as possible in the application startup in order to provide as much value as possible.

**Optionally** configure the `IAppInfo` before building it.

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


## Examples

The [examples](https://github.com/Reyhn3/AppInfo/tree/main/examples) folder contains runnable demo projects for different types of host applications and usages:

* [Basic](https://github.com/Reyhn3/AppInfo/tree/main/examples/Basic)
<br/> The most rudimentary usage example.
* [Custom](https://github.com/Reyhn3/AppInfo/tree/main/examples/Custom)
<br/> Demonstrates how to customize the information and output.
* [Manual](https://github.com/Reyhn3/AppInfo/tree/main/examples/Manual)
<br/> Compares manual configuration vs fluent syntax.
* [Generic Host](https://github.com/Reyhn3/AppInfo/tree/main/examples/GenericHost)
<br/> Demonstrates integration with the [Generic Host](https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host?tabs=appbuilder).
* [Logging](https://github.com/Reyhn3/AppInfo/tree/main/examples/Logging)
<br/> Shows how simple it is to output to the logger of your choice.
* [Web](https://github.com/Reyhn3/AppInfo/tree/main/examples/Web)
<br/> Demonstrates the integration with an ASP.NET Core Web Host, and how the `IAppInfo` instance can be used in a request.
* [Azure Functions](https://github.com/Reyhn3/AppInfo/tree/main/examples/AzureFunction)
<br/> Demonstrates the integration with an Azure Functions app host.


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


# Design

There are two phases to this library: **information extraction** and **outputting**. The information extraction phase is responsible for gathering all the information about the application and its running conditions. The outputting phase is responsible for rendering the information toward the configured targets.

Gathering information is done by using a builder pattern. The `AppInfoBuilder` class collect the `IExtractor` objects that can be added using fluent convenience methods. These are then used to assemble the information into a single `AppInfo` object that can be used by the outputting phase.

Outputting is also done by using a builder pattern. The `AppInfoOutputBuilder` class collect the `IOutputter` objects that can be added using fluent convenience methods. When calling the `Write` method, the `AppInfoOutputBuilder` will use the `IOutputter` objects to render the information toward the configured targets.
