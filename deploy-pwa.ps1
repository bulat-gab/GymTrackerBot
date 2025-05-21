# Path setup
$projectPath = "src\GymTrackerBot.PWA"
$outputPath = "build"
$docsPath = "docs"

# Step 1: Build the Blazor WebAssembly app
Write-Host "Building Blazor WASM project..."
dotnet publish $projectPath -c Release -o $outputPath

if ($LASTEXITCODE -ne 0) {
    Write-Error "Build failed. Aborting."
    exit $LASTEXITCODE
}

# Step 2: Clear and copy to ./docs
Write-Host "Updating ./docs folder..."
if (Test-Path $docsPath) {
    Remove-Item -Recurse -Force "$docsPath\*"
} else {
    New-Item -ItemType Directory -Path $docsPath | Out-Null
}

Copy-Item -Recurse -Force "$outputPath\wwwroot\*" $docsPath

# Step 3: Git commit and push
Write-Host "Committing and pushing changes..."
git add $docsPath
$commitMessage = "Deploy PWA to GitHub Pages - $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')"
git commit -m $commitMessage
git push

Write-Host "Deployment complete." -ForegroundColor Green
