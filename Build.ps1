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
    dotnet build "$Solution" -c "$Configuration"
}

if ($Test)
{
    dotnet test "$Solution" -c "$Configuration" --no-build --logger trx --results-directory "$TestResultsDirectory"
}