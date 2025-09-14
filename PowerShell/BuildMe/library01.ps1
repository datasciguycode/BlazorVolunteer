# library01.ps1 - Generic functions

# ---------------------------------------------------------------------------

function Update-TextInFile {
    param
    (
        $p_strFilePath
        , $p_strOldText
        , $p_strNewText
    )

    $content = Get-Content $p_strFilePath
    $content.Replace($p_strOldText, $p_strNewText) | Set-Content $p_strFilePath
}

# ---------------------------------------------------------------------------

function Add-ProgramReference {
    param 
    (
        $p_strProgramReference
    )

    $strOldText = "using Volunteer.Components;"
    $strNewText = @"
using Volunteer.Components;
$p_strProgramReference
"@

    Update-TextInFile -p_strFilePath ".\Program.cs" -p_strOldText $strOldText -p_strNewText $strNewText
}

# ---------------------------------------------------------------------------

function Add-ServiceReference {
    param 
    (
        $p_strServiceReference
    )

    $strOldText = "// Add services to the container."
    $strNewText = @"
// Add services to the container.
$p_strServiceReference
"@

    Update-TextInFile -p_strFilePath "Program.cs" -p_strOldText $strOldText -p_strNewText $strNewText
}

# ---------------------------------------------------------------------------

function Add-FluentNavLink {
    param 
    (
        $p_strNavLink
    )

    $strOldText = "        </FluentNavMenu>"
    $strNewText = @"
            $p_strNavLink
        </FluentNavMenu>
"@

    Update-TextInFile -p_strFilePath ".\Components\Layout\NavMenu.razor" -p_strOldText $strOldText -p_strNewText $strNewText
}

# ---------------------------------------------------------------------------

