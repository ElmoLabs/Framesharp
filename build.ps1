$current_directory = Split-Path -parent $MyInvocation.MyCommand.Definition
$msbuild = "C:\\windows\\microsoft.net\\framework64\\v4.0.30319\\MsBuild.exe"
$solution = Join-Path $current_directory 'Framesharp.sln'
$nuget = [io.path]::combine($current_directory, 'Nuget.exe')
$resources = Join-Path $current_directory 'Resources'
$output = '/p:OutputPath=' + $resources

If (!(Test-Path $nuget)) {
	(New-Object System.Net.WebClient).DownloadFile("https://dist.nuget.org/win-x86-commandline/latest/nuget.exe", $nuget)
}

& $nuget 'restore' $solution

& $msbuild $solution $output