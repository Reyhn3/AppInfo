#!/usr/bin/env pwsh

param(
	[Parameter(
		Mandatory=$true,
		Position=0,
		HelpMessage="The SemVer-compliant version of the package to use, e.g. `"1.2.3.4`"")]
	[version]
	$Version = "0.0.0",

	[Parameter(
		Mandatory=$false,
		Position=1,
		HelpMessage="The pre-release suffix to apply, e.g. `"dev-0`" (complete version will become `"1.2.3.4-dev-0`")")]
	[string]
	$PreRelease = $null
)

$FinalVersion = (($Version, $PreRelease) | ? {-not [string]::IsNullOrWhitespace($_)}) -join '-'
if (!$?) {
    Write-Host "`e[31mError when setting version!`e[0m"
    return
}
Write-Host "Setting version to `e[33m${FinalVersion}`e[0m."

$destination = "packages/${FinalVersion}"

Write-Host "`nPacking `e[35mAppInfo`e[0m."
dotnet pack "src/AppInfo/AppInfo.csproj" `
	--nologo --property WarningLevel=0 --force `
	--include-symbols `
	--runtime win-x64 `
	--configuration Release `
	--output $destination `
	-p:Version="${FinalVersion}" `
	-p:TargetFrameworks="net8.0"
if (!$?) {
    Write-Host "`e[31mError when packing `e[35mAppInfo`e[31m!`e[0m"
    return
}

Write-Host "`nSuccessfully packaged version `e[33m${FinalVersion}`e[0m to folder `e[36m${destination}`e[0m."
