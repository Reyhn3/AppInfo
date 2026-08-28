set shell := ["/usr/bin/env", "bash" ,"-c"]
set windows-shell := ["pwsh","-NoLogo", "-NoProfile", "-c"]
set quiet

[default]
[linux]
example:
	if [[ ! -f "./examples/Basic/bin/Debug/net8.0/basic" ]]; then echo "Building..."; dotnet build -v quiet; fi
	./examples/Basic/bin/Debug/net8.0/basic

[default]
[windows]
example:
	dotnet build -v quiet
	& .\examples\Basic\bin\Debug\net8.0\basic.exe


[linux]
[doc("Run all tests")]
test:
	dotnet test

[windows]
test:
	dotnet test


[linux]
[doc("Pack projects as NuGets")]
pack version prerelease-suffix="":
	echo "Packing v{{version}}{{ if prerelease-suffix == "" { "" } else { " as pre-release" } }}"
	./tools/pack.sh {{version}} {{prerelease-suffix}}

[windows]
pack version prerelease-suffix="":
	Write-Host "Packing v{{version}}{{ if prerelease-suffix == "" { "" } else { " as pre-release" } }}"
	.\tools\pack.ps1 {{version}} {{prerelease-suffix}}
