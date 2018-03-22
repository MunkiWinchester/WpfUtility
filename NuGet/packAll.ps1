Param
    (
        [Parameter(Position=0, Mandatory = $true, HelpMessage="Please specify a version", ValueFromPipeline = $true)]
        $version
    )

function SetConfig($file, $key, $value) {
    $content = Get-Content $file;

    if ( $content -match $key ) {
        "Edited version in $file to $value";
        $content -replace $key, $value |
        Set-Content $file;
    } else {
        "Can't find string. There may be some problems with your version";
        throw;
    }
}

function ChangeVersion ($path) {
    SetConfig $path "<version>.*</version>" "<version>$version</version>";
} 

ChangeVersion(Resolve-Path ..\.\WpfUtility\WpfUtility.nuspec);
.\nuget.exe pack ..\.\WpfUtility\WpfUtility.csproj -properties Configuration=Release;