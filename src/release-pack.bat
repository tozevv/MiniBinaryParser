set NugetPath= ..\tools
set MSBuildPath= C:\Windows\Microsoft.NET\Framework64\v4.0.30319

%MSBuildPath%\msbuild.exe MiniBinaryParser.sln /p:Configuration=Release /target:Clean,Build
%NugetPath%\nuget pack MiniBinaryParser.nuspec

