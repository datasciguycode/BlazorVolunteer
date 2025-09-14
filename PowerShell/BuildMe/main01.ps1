# main.01.ps1

# Recipe for building the application:

# Read "readme.md" in this folder before running this script.

# -----------------------------------------------------------------------------

Clear-Host

# Copy .gitignore to Solution folder:  # https://github.com/github/gitignore/blob/main/VisualStudio.gitignore
Copy-Item -Path .\PowerShell\CopyMe\.gitignore -Destination .\

# Copy .md files
New-Item -ItemType Directory -Path .\.github
Copy-Item -Path .\PowerShell\CopyMe\Copilot\copilot-instructions.md -Destination .\.github

# Copy .json files
New-Item -ItemType Directory -Path .\.vscode
Copy-Item -Path .\PowerShell\CopyMe\Copilot\launch.json -Destination .\.vscode
Copy-Item -Path .\PowerShell\CopyMe\Copilot\settings.json -Destination .\.vscode
Copy-Item -Path .\PowerShell\CopyMe\Copilot\tasks.json -Destination .\.vscode

# Load scripts
Set-Location "PowerShell\BuildMe"

# Load PowerShell code into memory
. .\main02.ps1

# Go to the Solution folder
cd..; cd..

# Install templates for new project
dotnet new install Microsoft.FluentUI.AspNetCore.Templates

# Create a new project
dotnet new fluentblazor -n $_strProject

# Go to the project folder
Set-Location $_strProject

# Install FluentUI components
dotnet add package Microsoft.FluentUI.AspNetCore.Components

# Add DB Connections (appsettings.json, appsettings.Development.json")
Add-Connections -p_strConnection $_strConnection

# Add EF Models and DbContext (Open SQL Server)
Install-EntityFramework -p_AddModels $true -p_strProjectFilePath $_strProjectFilePath

# Update EF
dotnet tool update --global dotnet-ef

# Add standard references to Program.cs
$strReference = @"
using Microsoft.EntityFrameworkCore;
using $_strProject.Models;
using $_strProject.Services;
"@
Add-ProgramReference -p_strProgramReference $strReference

# Add database context service reference
$strReference = @"
builder.Services.AddFluentUIComponents();

builder.Services.AddDbContext<${_strDatabase}Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
"@
Add-ServiceReference -p_strServiceReference $strReference

# Create a user table service reference
$strTableName = "User"
Create-InterfaceFile -p_strModelName $strTableName -p_strNamespace $_strProject -p_strOutputDir "Services"
Create-ServiceFile -p_strModelName $strTableName -p_strNamespace $_strProject -p_strOutputDir "Services"
Add-ServiceReference -p_strServiceReference "builder.Services.AddScoped<I${strTableName}Service, ${strTableName}Service>();"

# Create an Interest table service reference
$strTableName = "Interest"
Create-InterfaceFile -p_strModelName $strTableName -p_strNamespace $_strProject -p_strOutputDir "Services"
Create-ServiceFile -p_strModelName $strTableName -p_strNamespace $_strProject -p_strOutputDir "Services"
Add-ServiceReference -p_strServiceReference "builder.Services.AddScoped<I${strTableName}Service, ${strTableName}Service>();"

# Add link pointing to the User page
Add-FluentNavLink -p_strNavLink @"
<FluentNavLink Href="user" Icon="@(new Icons.Regular.Size20.Person())" IconColor="Color.Accent">Volunteer</FluentNavLink>
"@

# Add link pointing to the User page
Add-FluentNavLink -p_strNavLink @"
<FluentNavLink Href="interest" Icon="@(new Icons.Regular.Size20.Emoji())" IconColor="Color.Accent">Interest</FluentNavLink>
"@

<#
    # If needed:
    trust dev cert (Windows will prompt for UAC)
    dotnet dev-certs https --clean
    dotnet dev-certs https --trust
#>

dotnet build

# Human:  Setup local SQL Server 'Named Pipes' connection:
<#
    Open SQL Server Configuration Manager.
    Go to SQL Server Network Configuration > Protocols for SQLEXPRESS.
    Right-click 'Named Pipes' Select 'Enable'.    
    Restart the SQL Server (SQLEXPRESS) service.
#>

# Rebuild EF Models (when needed):
# Run this script in SQL Server to determine the named pipe (if needed):  EXEC xp_readerrorlog 0, 1, N'pipe';
<#
    $connectionString = 'Server=np:\\.\pipe\MSSQL$SQLEXPRESS\sql\query;Database=Volunteer;Trusted_Connection=True;TrustServerCertificate=true'
    dotnet ef dbcontext scaffold $connectionString Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --force --no-pluralize --data-annotations
#>


# Copy custom files from the "CopyMe" folder:
Copy-Item -Path ..\PowerShell\CopyMe\UserCopy.cs -Destination .\Models\UserCopy.cs
Copy-Item -Path ..\PowerShell\CopyMe\User.razor.css -Destination .\Components\Pages\User.razor.css
Copy-Item -Path ..\PowerShell\CopyMe\User.razor -Destination .\Components\Pages\User.razor
Copy-Item -Path ..\PowerShell\CopyMe\Home.razor -Destination .\Components\Pages\Home.razor
Copy-Item -Path ..\PowerShell\CopyMe\MainLayout.razor -Destination .\Components\Layout\MainLayout.razor
Copy-Item -Path ..\PowerShell\CopyMe\NavMenu.razor -Destination .\Components\Layout\NavMenu.razor

# Copy SQL files:
New-Item -ItemType Directory -Path .\Models\Sql
Copy-Item -Path ..\PowerShell\CopyMe\User.sql -Destination .\Models\Sql\User.sql

dotnet build

dotnet run

#  Run Project
<#
    Clear-Host

    cd..
    Set-Location "Powershell\BuildMe"
    . .\main02.ps1

    # Go to the project folder
    cd..; cd..
    Set-Location $_strProject

    # HTTPS + HTTP run:
    dotnet run --urls "https://localhost:5000;http://localhost:5001"

    dotnet watch run --urls "https://localhost:5000;http://localhost:5001"
#>


