Write-Host ""
Write-Host "=== FIXING REPOSITORIES (ASCII SAFE VERSION) ==="
Write-Host ""

$domainEntityPath = "Winperax.Domain\Entities"
$infrastructureRepoPath = "Winperax.Infrastructure\Repositories"

# Load entity names
$entities = Get-ChildItem $domainEntityPath -Filter "*.cs" | Select-Object -ExpandProperty BaseName

Write-Host "Detected Entities:"
foreach ($e in $entities) {
    Write-Host (" - " + $e)
}

Write-Host ""
Write-Host "Starting repository fixes..."
Write-Host ""

$repoFolders = Get-ChildItem $infrastructureRepoPath -Directory

foreach ($folder in $repoFolders) {

    $module = $folder.Name
    $repoFiles = Get-ChildItem $folder.FullName -Filter "*.cs"

    foreach ($file in $repoFiles) {

        $content = Get-Content $file.FullName -Raw

        $entityName = $entities | Where-Object { $_ -like "$module*" }

        if (-not $entityName) {
            Write-Host ("WARNING: No matching entity for module: " + $module)
            continue
        }

        Write-Host ("Fixing repository file: " + $file.Name + " using entity: " + $entityName)

        # Fix class name
        $content = $content -replace "class\s+[^\s]+", "class ${module}Repository"

        # Fix interface name
        $content = $content -replace "interface\s+[^\s]+", "interface I${module}Repository"

        # Fix constructor name
        $content = $content -replace "public\s+[^\(]+", "public ${module}Repository"

        # Fix IRepository logger types
        $content = $content -replace "ILogger<.*?>", "ILogger<${module}Repository>"

        # Fix generic collection calls
        $content = $content -replace "GetCollection<.*?>", "GetCollection<${entityName}>"

        # Fix Task return types
        $content = $content -replace "Task<.*?>", "Task<${entityName}>"
        $content = $content -replace "IEnumerable<.*?>", "IEnumerable<${entityName}>"

        # Ensure using exists
        if ($content -notmatch "using Winperax.Domain.Entities") {
            $content = "using Winperax.Domain.Entities;" + "`n" + $content
        }

        Set-Content -Path $file.FullName -Value $content -Encoding ASCII
    }
}

Write-Host ""
Write-Host "=== FIX COMPLETED ==="
Write-Host "You can now run: dotnet build"
Write-Host ""
