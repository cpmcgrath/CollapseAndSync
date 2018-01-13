function Replace($file, $before, $after)
{
    $content = Get-Content $file | Foreach-Object {$_ -replace $before, $after }
    $content | Set-Content $file -Encoding UTF8
}

function UpdateVersion($project, $version)
{
    $file = "./$project/Properties/AssemblyInfo.cs"
    Replace $file "AssemblyVersion\s*\([^\)]+\)"     "AssemblyVersion    (`"$version`")"
    Replace $file "AssemblyFileVersion\s*\([^\)]+\)" "AssemblyFileVersion(`"$version`")"
}

function UpdateVsVersion($project, $version)
{
    UpdateVersion $project $version
        
    Replace "./$project/CollapseAndSyncPackage.cs" `
            "\[InstalledProductRegistration\(`"#110`", `"#112`", `"[^`"]+`", IconResourceID = 400\)\]" `
            "[InstalledProductRegistration(`"#110`", `"#112`", `"$version`", IconResourceID = 400)]"
        
    Replace "./$project/source.extension.vsixmanifest" `
            "Id=`"8a270170-a54e-4815-86b6-9c0a93963640`" Version=`"[^`"]+`"" `
            "Id=`"8a270170-a54e-4815-86b6-9c0a93963640`" Version=`"$version`""
}