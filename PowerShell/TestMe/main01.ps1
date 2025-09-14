# main.01.ps1

# Instructions: Open in the Solutions folder.

# -----------------------------------------------------------------------------

Clear-Host

# Load scripts
Set-Location "PowerShell\TestMe"

# Load code into memory
. .\main02.ps1

# Navigate to the Test folder
cd..; cd..
Set-Location "Test"

# Create a new xUnit test project:
dotnet new xunit -n "$_strProject.Tests"

# Add the test project to the solution:
dotnet add "$_strProject.Tests" reference "../$_strProject/$_strProject.csproj"

# Add a reference to your main project:
dotnet add "$_strProject.Tests/$_strProject.Tests.csproj" reference "../$_strProject/$_strProject.csproj"

# Add specific versions compatible with .NET 9.0.302
dotnet add "$_strProject.Tests" package Microsoft.NET.Test.Sdk --version "17.11.1"
dotnet add "$_strProject.Tests" package xunit --version "2.9.0"
dotnet add "$_strProject.Tests" package xunit.runner.visualstudio --version "2.8.2"

# Add necessary NuGet packages for Blazor testing:
dotnet add "$_strProject.Tests" package Microsoft.AspNetCore.Mvc.Testing --version "9.0.0"
dotnet add "$_strProject.Tests" package AngleSharp --version "1.1.2"
dotnet add "$_strProject.Tests" package bunit --version "1.30.3"
dotnet add "$_strProject.Tests" package Microsoft.EntityFrameworkCore.InMemory --version "9.0.0"

# Show installed package versions
Write-Host "Installed test packages:" -ForegroundColor Green
dotnet list "$_strProject.Tests" package

Set-Location "$_strProject.Tests"

# Add tests
dotnet test


<#

dotnet remove package Microsoft.AspNetCore.Mvc.Testing
dotnet remove package Microsoft.EntityFrameworkCore.InMemory

dotnet add package Microsoft.AspNetCore.Mvc.Testing --version 9.0.7
dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 9.0.7

dotnet remove package Microsoft.NET.Test.Sdk
dotnet add package Microsoft.NET.Test.Sdk --version 17.11.1

winget install Microsoft.DotNet.Runtime.9

#>