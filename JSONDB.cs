using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Text.Json;
using Extensions;
using Microsoft.Extensions.Configuration;

class JSONDB<T, Y> : IDB<T, Y> where Y : notnull {
    private readonly string? _filePath;
    private ConcurrentDictionary<Y, T> _values;

    public JSONDB(IConfiguration config, string fileKey) {
        _filePath = config[$"DBFiles:{fileKey}"];
        Console.WriteLine(_filePath);
        _values = Read();
    }

    public ConcurrentDictionary<Y, T> Read() {
        if (!File.Exists(_filePath))
            throw new DBFileNotFoundException("DB file not found");

        string fileText = File.ReadAllText(_filePath!);
        if (fileText.Trim().Length == 0)
            return [];

        if (!fileText.IsJSON())
            throw new JSONFormatException("Format Error in DB files");

        return JsonSerializer.Deserialize<ConcurrentDictionary<Y, T>>(fileText)!;
    }

    private void Write() {
        // using will close the stream once we finish
        using FileStream file = new(_filePath!, FileMode.Create, FileAccess.Write, FileShare.Read);
        var rawData = JsonSerializer.Serialize<ConcurrentDictionary<Y, T>>(_values);
        // stream write accepts the array of bytes representing the data
        byte[] bytes = Encoding.UTF8.GetBytes(rawData);
        file.Write(bytes, 0, bytes.Length);
    }

    public void Edit(ConcurrentDictionary<Y, T> data) {
        _values = data;
        Write();
    }
}