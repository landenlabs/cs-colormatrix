ColorMatrix

This repository contains a Windows Forms demo application `ColorMatrix` that demonstrates how to apply a 5x5 color matrix to an image using `System.Drawing.Imaging.ColorMatrix`.

Binary filter format

To modernize filter storage this project uses a compact binary format for saved filters. Files and archives produced by build scripts and the application follow these rules:

- A single filter consists of:
  - `label.txt` — UTF-8 text label for the filter
  - `table.bin` — binary table of 25 `float32` values in row-major order (5 rows × 5 columns). Little-endian encoding is used. The values map directly to the `ColorMatrix` constructor.
  - `image.png` — optional PNG image representing the sample result

- Filters may be packaged in a ZIP archive (example: `filters.gzip`) using one folder per filter (e.g. `filter0/label.txt`, `filter0/table.bin`, `filter0/image.png`). The application can read such archives from embedded resources or from disk.

- Companion file format when saving a single filter on disk:
  - The app may save a companion `.bin` file next to a filter to store the 25 float values. The image (PNG) is saved using the same base filename with `.png` extension.

Backward compatibility and fallbacks

- The application will also accept `table.csv`/`table.txt` content if present in the archive and will parse numeric tokens from arbitrary text.
- Legacy GZip-packed filter blobs are supported where the stream contains a sequence of length-prefixed items. The loader detects a `table.bin` (binary) marker and will read 25 floats when present.

Using the PowerShell script

- The repository includes `make-filters-zip.ps1`. Run it with PowerShell to generate `filters.gzip` from XML files in the `Filters` directory:

  - Default: `powershell -NoProfile -ExecutionPolicy Bypass -File .\make-filters-zip.ps1`
  - Specify input/output: `powershell -NoProfile -File .\make-filters-zip.ps1 -In "Samples\Filters" -Out "my-filters.gzip"`

.NET console validator

- A small .NET console tool is provided at `tools/ValidateFiltersGzip.cs` to validate `filters.gzip` contents programmatically.

  Build and run:
    dotnet build tools/ValidateFiltersGzip.csproj
    dotnet run --project tools/ValidateFiltersGzip.csproj -- filters.gzip

Author

Dennis Lang (landenlabs)

License

MIT
