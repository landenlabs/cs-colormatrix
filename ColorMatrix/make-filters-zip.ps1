<#
.SYNOPSIS
Create a filters.gzip archive from XML-based filter files by emitting a compact binary `table.bin`.

.DESCRIPTION
This script scans a Filters directory (or a provided input directory) for `*.xml` files,
extracts up to 25 numeric tokens from each file and writes them as 25 little-endian float32
values to a `table.bin` entry in a ZIP archive. A `label.txt` (UTF8) entry and an optional
`image.png` companion file are also added to the filter folder in the archive.

USAGE
    .\make-filters-zip.ps1 [-In <FiltersDir>] [-Out <ArchivePath>]

Examples
    # default: read Filters/*.xml and produce filters.gzip
    .\make-filters-zip.ps1

    # specify input directory and output file
    .\make-filters-zip.ps1 -In "Samples\Filters" -Out "my-filters.gzip"

NOTES
- Requires PowerShell with access to System.IO.Compression.FileSystem (Windows PowerShell or pwsh).
- The script is idempotent: it removes an existing output file before creating a new one.
#>

param(
    [string]$In = 'Filters',
    [string]$Out = 'filters.gzip'
)

Write-Host "make-filters-zip.ps1 - input: $In  output: $Out"

if (-not (Get-Command Add-Type -ErrorAction SilentlyContinue)) {
    Write-Error "PowerShell environment missing required cmdlets."
    exit 2
}

try {
    Add-Type -AssemblyName System.IO.Compression.FileSystem -ErrorAction Stop
} catch {
    Write-Error "Failed to load System.IO.Compression.FileSystem: $_"
    exit 2
}

if (-not (Test-Path $In)) {
    Write-Error "Input directory '$In' not found."
    exit 1
}

if (Test-Path $Out) {
    Write-Host "Removing existing output '$Out'"
    Remove-Item $Out -Force
}

try {
    $zip = [System.IO.Compression.ZipFile]::Open($Out,'Create')
} catch {
    Write-Error "Failed to create archive '$Out': $_"
    exit 3
}

$i = 0
Get-ChildItem -Path $In -Filter *.xml -File | ForEach-Object {
    $xmlFile = $_.FullName
    $folder = "filter$($i++)/"
    Write-Host "Processing: $($_.Name) -> $folder"

    # Read xml/text and extract float tokens
    $text = Get-Content $xmlFile -Raw -ErrorAction SilentlyContinue
    $matches = [regex]::Matches($text, '[-+]?\d*\.?\d+(?:[eE][-+]?\d+)?')
    $vals = New-Object System.Collections.Generic.List[Single]
    foreach ($m in $matches) {
        try {
            $f = [single]::Parse($m.Value, [System.Globalization.CultureInfo]::InvariantCulture)
            $vals.Add($f)
        } catch {
            # skip unparsable token
        }
        if ($vals.Count -ge 25) { break }
    }

    # If we didn't get 25 values, fall back to identity matrix
    if ($vals.Count -lt 25) {
        $vals.Clear()
        for ($k = 0; $k -lt 25; $k++) {
            if ($k -in @(0,6,12,18,24)) { $vals.Add([single]1.0) } else { $vals.Add([single]0.0) }
        }
    } else {
        # ensure exactly 25 items
        while ($vals.Count -gt 25) { $vals.RemoveAt($vals.Count - 1) }
    }

    # create binary table.bin entry and write 25 float32 values (little-endian)
    $entry = $zip.CreateEntry($folder + 'table.bin', [System.IO.Compression.CompressionLevel]::Optimal)
    $s = $entry.Open()
    $bw = New-Object System.IO.BinaryWriter($s)
    try {
        foreach ($f in $vals) { $bw.Write([single]$f) }
    } finally {
        $bw.Close()
        $s.Close()
    }

    # add label.txt
    $labelEntry = $zip.CreateEntry($folder + 'label.txt')
    $s2 = $labelEntry.Open()
    try {
        $bytes = [System.Text.Encoding]::UTF8.GetBytes($_.BaseName)
        $s2.Write($bytes, 0, $bytes.Length)
    } finally {
        $s2.Close()
    }

    # optional image.png companion
    $img = [System.IO.Path]::ChangeExtension($xmlFile, '.png')
    if (Test-Path $img) {
        [System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile($zip, $img, $folder + 'image.png', [System.IO.Compression.CompressionLevel]::Optimal)
    }
}

$zip.Dispose()
Write-Host "Wrote $Out"
