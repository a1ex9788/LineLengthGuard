param (
    [switch]$Build,
    [switch]$Test,
    [switch]$Publish,
    [switch]$Upload,

    [string]$Configuration = "Release",
    [string]$TestResultsDirectory = "TestResults",
    [string]$PublicationDirectory = "Publish",
    [string]$Version = "9.9",
    [string]$NuGetApiKey = "API key"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

if ((-not $Build) -and (-not $Test) -and (-not $Publish) -and (-not $Upload))
{
    Write-Warning "Activate any flag (-Build | -Test | -Publish | -Upload) to perform some action."

    exit 0
}

$SolutionDirectory = Join-Path $PSScriptRoot "Code"
$Solution = Join-Path $SolutionDirectory "LineLengthGuard.sln"

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

if ($Publish)
{
    $packageProject = Join-Path $SolutionDirectory "Source" "LineLengthGuard.Package" "LineLengthGuard.Package.csproj"
    $PublicationDirectory = [System.IO.Path]::GetFullPath($PublicationDirectory)

    dotnet build "$packageProject" -c "$Configuration" `
        /p:GeneratePackageOnBuild=true /p:PackageVersion=$Version /p:PackageOutputPath=$PublicationDirectory
}

if ($Upload)
{
    $package = Join-Path $PublicationDirectory "LineLengthGuard.$Version.nupkg"

    dotnet nuget push "$package" --source "https://api.nuget.org/v3/index.json" --api-key "$NuGetApiKey"
}