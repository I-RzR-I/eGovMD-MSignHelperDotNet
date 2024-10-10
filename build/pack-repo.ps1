Param
(
	[Parameter(Mandatory = $false)]
	[string]$ownVersion,
	[Parameter(Mandatory = $false)]
	[bool]$runTest
);

$assemblyPath = "..\src\shared\GeneralAssemblyInfo.cs";
$defaultVersion = "1.0.0.0";
$nugetPath = "../nuget";
$data = ("..\src\MSignHelperDotNet\MSignHelperDotNet.csproj");
$testExec = $false;

<#
	.SYNOPSIS
		A brief description of the Get-CurrentAssemblyVersion function.
	
	.DESCRIPTION
		Get current assembly version
	
	.EXAMPLE
				PS C:\> Get-CurrentAssemblyVersion
	
	.NOTES
		Additional information about the function.
#>
function Get-CurrentAssemblyVersion
{
	[OutputType([string])]
	param ()
	
	$assemblyInfo = (Get-Content $assemblyPath);
	$asVersion = ($assemblyInfo -match 'AssemblyVersion\(".*"\)');
	$asVersion = $asVersion -split ('"');
	$asVersion = $asVersion[1];
	
	return $asVersion;
}

<#
	.SYNOPSIS
		A brief description of the Build-And-Pack-BuildPack function.
	
	.DESCRIPTION
		Build project and pack as package (u pkg)
	
	.PARAMETER packVersion
		Package/Build version
	
	.PARAMETER currentVersion
		A description of the currentVersion parameter.
	
	.EXAMPLE
		PS C:\> Build-And-Pack-BuildPack -packVersion 'Value1'
	
	.NOTES
		Additional information about the function.
#>
function Set-BuildAndPack
{
	[CmdletBinding()]
	[OutputType([bool])]
	param
	(
		[Parameter(Mandatory = $true)]
		[string]$packVersion,
		[string]$currentVersion
	)
	
	try
	{
		Write-Host "Project restore '$($_)'!" -ForegroundColor Green;
		dotnet restore $($_);
		
		Write-Host "Build in Release '$($_)'!" -ForegroundColor Green;
		$buildResult = dotnet build $($_) --source https://api.nuget.org/v3/index.json -c Release /p:AssemblyVersion=$packVersion /p:AssemblyFileVersion=$packVersion /p:AssemblyInformationalVersion=$packVersion;
		if ($LASTEXITCODE -ne 0)
		{
			Set-VersionAssembly -packVersion $currentVersion;
			Write-Host $buildResult;
			
			return $false;
		}
		
		Write-Host "Pack in Release '$($_)'!" -ForegroundColor Green;
		$packResult = dotnet pack $($_) -p:PackageVersion=$packVersion --no-build -c Release --output $nugetPath;
		if ($LASTEXITCODE -ne 0)
		{
			Set-VersionAssembly -packVersion $currentVersion;
			Write-Host $buildResult;
			
			return $false;
		}
		
		return $true;
	}
	catch
	{
		Write-Host -foregroundcolor Red "An error occurred: $_"
		
		return $false;
	}
}

<#
	.SYNOPSIS
		A brief description of the Get-TimeStamp function.
	
	.DESCRIPTION
		Get time stamp version
	
	.EXAMPLE
				PS C:\> Get-TimeStamp
	
	.NOTES
		Additional information about the function.
#>
function Get-TimeStamp
{
	[CmdletBinding()]
	[OutputType([int])]
	param ()
	
	$current = [System.DateTime]::Now;
	$end = [System.DateTime]::Now.Date;
	$diff = (New-TimeSpan -Start $current -End $end).TotalSeconds / 10;
	$timeSec = If ($diff -le 0) { $diff * -1 }
	Else { $diff };
	
	return [int]$timeSec;
}

<#
	.SYNOPSIS
		A brief description of the Set-VersionAssembly function.
	
	.DESCRIPTION
		Set current version in assembly file
	
	.PARAMETER packVersion
		A description of the packVersion parameter.
	
	.EXAMPLE
				PS C:\> Set-VersionAssembly -packVersion 'Value1'
	
	.NOTES
		Additional information about the function.
#>
function Set-VersionAssembly
{
	[CmdletBinding()]
	[OutputType([void])]
	param
	(
		[Parameter(Mandatory = $true)]
		[string]$packVersion
	)
	$NewVersion = 'AssemblyVersion("' + $packVersion + '")';
	$NewFileVersion = 'AssemblyFileVersion("' + $packVersion + '")';
	$NewAssemblyInformationalVersion = 'AssemblyInformationalVersion("' + $packVersion + '")';
	
	(Get-Content $assemblyPath -encoding utf8) |
	%{ $_ -replace 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $NewVersion } |
	%{ $_ -replace 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $NewFileVersion } |
	%{ $_ -replace 'AssemblyInformationalVersion\("[0-9x]+(\.([0-9x]+|\*)){1,3}"\)', $NewAssemblyInformationalVersion } |
	Set-Content $assemblyPath -encoding utf8
}

<#
	.SYNOPSIS
		A brief description of the Exec-TestSolution function.
	
	.DESCRIPTION
		Execute solution test
	
	.EXAMPLE
				PS C:\> Exec-TestSolution
	
	.NOTES
		Additional information about the function.
#>
function Exec-TestSolution
{
	[CmdletBinding()]
	[OutputType([bool])]
	param ()
	
	# Merge all streams into stdout
	#$result = dotnet test "..\src\tests\*.csproj" *>&1
	
	#No test
	return $true;
	
	# Evaluate success/failure
	if ($LASTEXITCODE -eq 0)
	{
		return $true;
	}
	else
	{
		$errorString = $result -join [System.Environment]::NewLine;
		Write-Host -foregroundcolor Red "An error occurred: $errorString";
		
		return $false;
	}
}

If ($runTest -eq $true)
{
	Write-Host "Init test solution...`n" -ForegroundColor Green;
	$testExec = Exec-TestSolution;
}
Else { $testExec = $true; }

If ($testExec -eq $true)
{
	Write-Host "Path to pack: '$nugetPath'`n" -ForegroundColor Green;
	
	$currentVersion = "";
	If ($ownVersion -eq $null -or $ownVersion -eq "") { $currentVersion = Get-CurrentAssemblyVersion; }
	Else { $currentVersion = $ownVersion; }
		
	$directoryInfo = Get-ChildItem $nugetPath | Where-Object { $_.Name -match '[a-z]*.1.0.0.nupkg$' } | Measure-Object;
	If ($defaultVersion -eq $currentVersion -and $directoryInfo.count -eq 0)
	{
		Set-VersionAssembly -packVersion $currentVersion;
		
		$data | ForEach-Object	{
			$buildResult = Set-BuildAndPack -packVersion $currentVersion;
			If ($buildResult -eq $false -or $buildResult -contains $false)
			{
				Write-Host "`nBuild/pack failed!!!" -ForegroundColor Red;
				
				exit;
			}			
		}
		
		Write-Host "`nPack executed with success with version: $currentVersion!" -ForegroundColor Green;
		
		exit;
	}
	Else
	{
		$finalVersion = "";
		If ($ownVersion -eq $null -or $ownVersion -eq "")
		{
			$versArray = $currentVersion.Split('.');
			$finalVersion = $versArray[0].ToString() + "." + $versArray[1].ToString() + "." + (([int]$versArray[2]) + 1).ToString() + "." + (Get-TimeStamp).ToString();
		}
		Else { $finalVersion = $ownVersion; }
		
		Set-VersionAssembly -packVersion $finalVersion;
		
		$data | ForEach-Object	{
			$buildResult = Set-BuildAndPack -packVersion $finalVersion -currentVersion $currentVersion;
			If ($buildResult -eq $false -or $buildResult -contains $false)
			{
				Write-Host "`nBuild/pack failed!!!" -ForegroundColor Red;
				
				exit;
			}
		}
		
		Write-Host "`nPack executed with success with version: $finalVersion!" -ForegroundColor Green;
		
		exit;
	}
}
Else { exit; }