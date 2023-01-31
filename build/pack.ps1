$asCfg = $args[0]
if($asCfg -eq 'Release')
{
    $assemblyInfo = (Get-Content ..\shared\GeneralAssemblyInfo.cs)

    $asVersion = ($assemblyInfo -match 'AssemblyVersion\(".*"\)')
    $asVersion = $asVersion -split ('"')
    $asVersion = $asVersion[1]

    $nugetPath = "../../nuget"

    dotnet pack -p:PackageVersion=$asVersion --no-build -c Release --output $nugetPath 
}
else {Write-Host "Solution not in 'Release' mode. Received:" $asCfg}