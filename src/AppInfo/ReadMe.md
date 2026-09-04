# AppInfo

Collect, display and optionally save information about the host application during startup.


Usage:

```csharp
using AppInformation;

AppInfo.BuildAndOutputDefault();
```

Output:

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

Read more about the package [in the repository](https://github.com/Reyhn3/AppInfo/blob/main/README.md).
