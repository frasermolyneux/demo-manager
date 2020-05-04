##-----------------------------------------------------------------------
## Based on: https://msdn.microsoft.com/Library/vs/alm/Build/scripts/index
##-----------------------------------------------------------------------
# Replace values in AssemblyInfo.cs with Company default ones and apply version number based on build.
$copy = [char]0x00A9
# Start Configuration
$DefaultAssemblyDescription = ""
$DefaultAssemblyCompany = "XtremeIdiots"
$DefaultAssemblyProduct = "Demo Manager"
$DefaultAssemblyCopyright = "Copyright " + $copy + " XtremeIdiots 2016"
# End Configuration
 
function FindOrReplaceAttribute($filecontent, $assemblyValue, $newValue)
{
	$regex = "\[assembly: Assembly" + $assemblyValue + "\((.+)\)\]"
	$replacement = '[assembly: Assembly' + $assemblyValue + '("' + $newValue + '")]'
	
    $propertyExists = [regex]::matches($filecontent, $regex)
    
    if ($propertyExists.Count -eq 1)
    {
        return $filecontent -replace $regex, $replacement
    }
    
    return $filecontent + $replacement
}
 
# Enable -Verbose option
[CmdletBinding()]
 
# Set a flag to force verbose as a default
$VerbosePreference ='Continue' # equiv to -verbose
 
# If this script is not running on a build server, remind user to 
# set environment variables so that this script can be debugged
if(-not ($Env:BUILD_SOURCESDIRECTORY -and $Env:BUILD_BUILDNUMBER))
{
    Write-Error "You must set the following environment variables"
    Write-Error "to test this script interactively."
    Write-Host '$Env:BUILD_SOURCESDIRECTORY - For example, enter something like:'
    Write-Host '$Env:BUILD_SOURCESDIRECTORY = "C:\code\FabrikamTFVC\HelloWorld"'
    Write-Host '$Env:BUILD_BUILDNUMBER - For example, enter something like:'
    Write-Host '$Env:BUILD_BUILDNUMBER = "Build HelloWorld_0000.00.00.0"'
    exit 1
}
 
# Make sure path to source code directory is available
if (-not $Env:BUILD_SOURCESDIRECTORY)
{
    Write-Error ("BUILD_SOURCESDIRECTORY environment variable is missing.")
    exit 1
}
elseif (-not (Test-Path $Env:BUILD_SOURCESDIRECTORY))
{
    Write-Error "BUILD_SOURCESDIRECTORY does not exist: $Env:BUILD_SOURCESDIRECTORY"
    exit 1
}
Write-Verbose "BUILD_SOURCESDIRECTORY: $Env:BUILD_SOURCESDIRECTORY"
 
# Make sure there is a build number
if (-not $Env:BUILD_BUILDNUMBER)
{
    Write-Error ("BUILD_BUILDNUMBER environment variable is missing.")
    exit 1
}
Write-Verbose "BUILD_BUILDNUMBER: $Env:BUILD_BUILDNUMBER"
 
# Get and validate the version data
$VersionData = [regex]::matches($Env:BUILD_BUILDNUMBER, "\d+\.\d+\.\d+\.\d+")
switch($VersionData.Count)
{
   0        
      { 
         Write-Error "Could not find version number data in BUILD_BUILDNUMBER."
         exit 1
      }
   1 {}
   default 
      { 
         Write-Warning "Found more than instance of version data in BUILD_BUILDNUMBER." 
         Write-Warning "Will assume first instance is version."
      }
}
$NewVersion = $VersionData[0]
Write-Verbose "Version: $NewVersion"
 
# Apply the version to the assembly property files
$files = gci $Env:BUILD_SOURCESDIRECTORY -recurse -include "*Properties*","My Project" | 
    ?{ $_.PSIsContainer } | 
    foreach { gci -Path $_.FullName -Recurse -include AssemblyInfo.* }
if($files)
{
    Write-Verbose "Will apply $NewVersion to $($files.count) files."
 
    foreach ($file in $files) {
        $filecontent = Get-Content($file)
        attrib $file -r
        $filecontent = FindOrReplaceAttribute $filecontent "Company" $DefaultAssemblyCompany
		$filecontent = FindOrReplaceAttribute $filecontent "Copyright" $DefaultAssemblyCopyright
		$filecontent = FindOrReplaceAttribute $filecontent "Description" $DefaultAssemblyDescription
		$filecontent = FindOrReplaceAttribute $filecontent "Product" $DefaultAssemblyProduct
		$filecontent = FindOrReplaceAttribute $filecontent "Version" $NewVersion
		$filecontent = FindOrReplaceAttribute $filecontent "FileVersion" $NewVersion
 
        $filecontent | Out-File $file
        Write-Verbose "$file - Assembly Changes Applied"
    }
}
else
{
    Write-Warning "Found no files."
}