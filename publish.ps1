& dotnet build Emergent.Challenge.sln --configuration Release 
& dotnet publish Emergent.Challenge.sln --configuration Release --output artifacts
Copy-Item -Path ".\artifacts\Emergent.Challenge.Web\dist\*" -Destination ".\docs\" -recurse -Force
