param (
    [switch]$Build,
    [switch]$Test,

    [string]$Configuration = "Release",
    [string]$TestResultsDirectory = "TestResults"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

if ((-not $Build) -and (-not $Test))
{
    Write-Warning "Activate any flag (-Build | -Test) to perform some action."

    exit 0
}

$Solution = Join-Path $PSScriptRoot "Code" "LineLengthGuard.sln"

if ($Build)
{
    # VSIX project has to be built with MSBuild.
    $msBuildLocation = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"

    if (Test-Path $msBuildLocation)
    {
        . "$msBuildLocation" "$Solution" /t:Restore /t:Build /p:Configuration=$Configuration /v:minimal
    }
    else
    {
        msbuild "$Solution" /t:Restore /t:Build /p:Configuration=$Configuration /v:minimal
    }
}

if ($Test)
{
    dotnet test "$Solution" -c "$Configuration" --no-build --logger trx --results-directory "$TestResultsDirectory"
}