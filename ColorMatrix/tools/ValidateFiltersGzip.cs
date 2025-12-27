using System;
using System.IO;
using System.IO.Compression;

// Simple .NET console utility to validate filters.gzip content programmatically.
// Use `dotnet run --project tools/ValidateFiltersGzip.csproj -- filters.gzip` to execute.

internal static class ValidateFiltersGzipTool
{
    // Non-entry-point method so this file won't introduce a second Main when compiled into the
    // main ColorMatrix project (some project files include sources recursively).
    public static int Run(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: ValidateFiltersGzip <path-to-gzip>");
            return 2;
        }

        string path = args[0];
        if (!File.Exists(path))
        {
            Console.WriteLine($"File not found: {path}");
            return 3;
        }

        try
        {
            using var fs = File.OpenRead(path);
            using var archive = new ZipArchive(fs, ZipArchiveMode.Read, leaveOpen: false);
            foreach (var entry in archive.Entries)
            {
                Console.WriteLine(entry.FullName);
            }

            // Group entries by top-level folder
            var folders = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<ZipArchiveEntry>>(StringComparer.OrdinalIgnoreCase);
            foreach (var entry in archive.Entries)
            {
                if (string.IsNullOrEmpty(entry.FullName)) continue;
                string top = entry.FullName;
                int idx = top.IndexOf('/');
                if (idx >= 0) top = top.Substring(0, idx + 1);
                if (!folders.TryGetValue(top, out var list)) { list = new System.Collections.Generic.List<ZipArchiveEntry>(); folders[top] = list; }
                list.Add(entry);
            }

            bool ok = true;
            foreach (var pair in folders)
            {
                bool hasTable = false;
                foreach (var e in pair.Value)
                {
                    if (e.FullName.EndsWith("table.bin", StringComparison.OrdinalIgnoreCase))
                    {
                        hasTable = true;
                        using var s = e.Open();
                        using var br = new BinaryReader(s);
                        try
                        {
                            for (int i = 0; i < 25; i++) br.ReadSingle();
                        }
                        catch (EndOfStreamException)
                        {
                            Console.WriteLine($"Invalid table.bin (too short) in {pair.Key}");
                            ok = false;
                        }
                        break;
                    }
                }
                if (!hasTable)
                {
                    Console.WriteLine($"Missing table.bin in folder {pair.Key}");
                    ok = false;
                }
            }

            Console.WriteLine(ok ? "Validation succeeded." : "Validation failed.");
            return ok ? 0 : 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return 4;
        }
    }
}
