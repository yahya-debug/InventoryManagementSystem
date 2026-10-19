using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Text.Json;


class DB<T, Y> where Y : notnull {
    private readonly string? _filePath;
    private ConcurrentDictionary<Y, T> _values;

    public DB(string file_key) {
        DotNetEnv.Env.TraversePath().Load();
        _filePath = Environment.GetEnvironmentVariable(file_key);
        _values = read();
    }

    public ConcurrentDictionary<Y, T> read() {

        if (File.Exists(_filePath)) {
            string file_txt = File.ReadAllText(_filePath!);
            if (file_txt.Trim().Length == 0)
                return new();

            if (!(file_txt.Trim().StartsWith("{") && file_txt.Trim().EndsWith("}") || (file_txt.Trim().StartsWith("[") && file_txt.Trim().EndsWith("]"))))
                throw new Exception("Format Error in DB files");

            return JsonSerializer.Deserialize<ConcurrentDictionary<Y, T>>(file_txt)!;
        }
        throw new Exception("DB file not found");

    }

    private void write() {
        // using will close the stream once we finish
        using (FileStream file = new FileStream(_filePath!, FileMode.Create, FileAccess.Write, FileShare.Read)) {
            string str_data = JsonSerializer.Serialize<ConcurrentDictionary<Y, T>>(_values);
            // stream write accepts the array of bytes representing the data
            byte[] bytes = Encoding.UTF8.GetBytes(str_data);
            file.Write(bytes, 0, bytes.Length);
        }
    }

    public void edit(ConcurrentDictionary<Y, T> data) {
        _values = data;
        write();
    }
}