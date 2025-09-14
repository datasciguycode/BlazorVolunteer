# main02.ps1

. .\config01.ps1
. .\library01.ps1

# ---------------------------------------------------------------------------

function Initialize-Git {
    dotnet new gitignore
    git init
    Write-Host "Git initialized."  
}

# ---------------------------------------------------------------------------

function Add-Connections {
    
    param (
        [string]$p_strConnection
    )
    
    Add-Connection -p_strConnection $p_strConnection -p_strFilePath "appsettings.json"
    Add-Connection -p_strConnection $p_strConnection -p_strFilePath "appsettings.Development.json"
}

# ---------------------------------------------------------------------------

function Add-Connection {
    param 
    (
        $p_strConnection
        , $p_strFilePath
    )

    $json = Get-Content -Path $p_strFilePath  | ConvertFrom-Json
    $json | Add-Member -Type NoteProperty -Name ConnectionStrings -Value @{DefaultConnection = $p_strConnection }
    $json | ConvertTo-Json -Depth 32 | Set-Content -Path $p_strFilePath
}

# ---------------------------------------------------------------------------

function Install-EntityFramework {
    param 
    (
        [bool]$p_AddModels
        , [string]$p_strProjectFilePath
    )

    $strConnection = "Name=ConnectionStrings:DefaultConnection"
    $strSqlServerPackage = "Microsoft.EntityFrameworkCore.SqlServer"

    # Add Entities
    Install-Package -p_strProjectFilePath $p_strProjectFilePath -p_strPackageName $strSqlServerPackage

    if ($p_AddModels) {
        Install-Package -p_strProjectFilePath $p_strProjectFilePath -p_strPackageName "Microsoft.EntityFrameworkCore.Design"
        dotnet ef dbcontext scaffold $strConnection $strSqlServerPackage --output-dir "Models" --force  --project $p_strProjectFilePath --data-annotations --use-database-names --no-pluralize
    }
}

# ---------------------------------------------------------------------------

function Install-Package {    
    param 
    (
        [string]$p_strProjectFilePath
        , [string]$p_strPackageName
    )

    $installedPackages = dotnet list $p_strProjectFilePath package | Out-String

    if (($installedPackages -match $p_strPackageName)) {       
        Write-Host "$p_strPackageName is already installed."
    }
    else {
        dotnet add $p_strProjectFilePath package $p_strPackageName
    }
}

# ---------------------------------------------------------------------------

function Create-ServiceFile {
    param
    (
        $p_strModelName = "User",
        $p_strNamespace = "Volunteer",
        $p_strOutputDir = ".\\Volunteer\\Services"
    )
    
    if (!(Test-Path $p_strOutputDir)) {
        New-Item -ItemType Directory -Path $p_strOutputDir | Out-Null
    }
    
    $serviceContent = @"
using $p_strNamespace.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $p_strNamespace.Services
{
    public class ${p_strModelName}Service : I${p_strModelName}Service
    {
        private readonly ${p_strNamespace}Context _context;

        // -----------------------------------------------------------------

        public ${p_strModelName}Service(${p_strNamespace}Context context)
        {
            _context = context;
        }

        // -----------------------------------------------------------------

        public async Task<List<$p_strModelName>> ToListAsync()
        {
            return await _context.${p_strModelName}.ToListAsync();
        }

        // -----------------------------------------------------------------

        public async Task<${p_strModelName}?> GetByIdAsync(int id)
        {
            return await _context.${p_strModelName}.FindAsync(id);
        }

        // -----------------------------------------------------------------

        public async Task AddAsync($p_strModelName $($p_strModelName.ToLower()))
        {
            _context.${p_strModelName}.Add($($p_strModelName.ToLower()));
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task UpdateAsync($p_strModelName $($p_strModelName.ToLower()))
        {
            _context.${p_strModelName}.Update($($p_strModelName.ToLower()));
            await _context.SaveChangesAsync();
        }

        // -----------------------------------------------------------------

        public async Task DeleteAsync(int id)
        {
            var $($p_strModelName.ToLower()) = await _context.${p_strModelName}.FindAsync(id);
            if ($($p_strModelName.ToLower()) != null)
            {
                _context.${p_strModelName}.Remove($($p_strModelName.ToLower()));
                await _context.SaveChangesAsync();
            }
        }

        // -----------------------------------------------------------------
    }
}
"@
    
    $servicePath = Join-Path $p_strOutputDir "${p_strModelName}Service.cs"
    Set-Content -Path $servicePath -Value $serviceContent
    
    Write-Host "Service file created:"
    Write-Host $servicePath    
}

# ---------------------------------------------------------------------------

function Create-InterfaceFile {
    param(
        [string]$p_strModelName = "User",
        [string]$p_strNamespace = "Volunteer",
        [string]$p_strOutputDir = ".\\Volunteer\\Services"
    )
    
    if (!(Test-Path $p_strOutputDir)) {
        New-Item -ItemType Directory -Path $p_strOutputDir | Out-Null
    }
    
    $interfaceContent = @"
using $p_strNamespace.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $p_strNamespace.Services
{
    public interface I${p_strModelName}Service
    {
        Task<List<$p_strModelName>> ToListAsync();
        Task<${p_strModelName}?> GetByIdAsync(int id);
        Task AddAsync($p_strModelName $($p_strModelName.ToLower()));
        Task UpdateAsync($p_strModelName $($p_strModelName.ToLower()));
        Task DeleteAsync(int id);
    }
}
"@
    
    $interfacePath = Join-Path $p_strOutputDir "I${p_strModelName}Service.cs"
    Set-Content -Path $interfacePath -Value $interfaceContent
    
    Write-Host "Interface file created:"
    Write-Host $interfacePath    
}

# ---------------------------------------------------------------------------
function New-DbTableWebPage {
    param(
        [string]$p_strTableName,
        [string]$p_strNamespace,
        [string]$p_strOutputDir
    )
    
    if (!(Test-Path $p_strOutputDir)) {
        New-Item -ItemType Directory -Path $p_strOutputDir | Out-Null
    }
    
    $strTableNameLower = $p_strTableName.ToLower()

    $strContent = @"
@page "/$strTableNameLower"
@inject $p_strNamespace.Services.I${p_strTableName}Service ${p_strTableName}Service

<PageTitle>$p_strTableName</PageTitle>

@if (userList == null)
{
    <p>Loading...</p>
}
else if (userList.Count == 0)
{
    <p>No records found.</p>
}
else
{
    <table class="table">
        <thead>
            <tr>
                <th>${p_strTableName}</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var user in userList)
            {
                <tr>
                    <td>@${strTableNameLower}.${p_strTableName}ID</td>
                </tr>
            }
        </tbody>
    </table>
}

@code {
    private List<Volunteer.Models.User>? ${strTableNameLower}List;

    protected override async Task OnInitializedAsync()
    {
        ${strTableNameLower}List = await UserService.GetAllAsync();
    }
}
"@
    
    $filePath = Join-Path $p_strOutputDir "${p_strTableName}.razor"
    Set-Content -Path $filePath -Value $strContent
    
    Write-Host "Table page created:"
    Write-Host $filePath    
}

# ---------------------------------------------------------------------------
