# Get all .cs files in the CharityAuction directories
$files = Get-ChildItem -Path CharityAuction* -Recurse -Include *.cs,*.csproj

foreach ($file in $files) {
    # Read the content of the file
    $content = Get-Content -Path $file.FullName -Raw
    
    # Replace all occurrences of ScholarSystem with CharityAuction
    $newContent = $content -replace "ScholarSystem", "CharityAuction"
    
    # Write the content back to the file
    Set-Content -Path $file.FullName -Value $newContent
}

Write-Host "Namespace replacements completed." 