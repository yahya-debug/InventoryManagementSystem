using System.IO;
using System.Text;
using System.Text.Json;


class DB<T> where T : new() {
    private readonly string? _filePath;
    private T _values;

    public DB(string file_key) {
        _values = new();
        _filePath = Environment.GetEnvironmentVariable(file_key);
    }

    private T read() {
        if (File.Exists(_filePath))
            return JsonSerializer.Deserialize<T>(File.ReadAllText(_filePath!))!;
        throw new Exception("DB file not found");
    }

    private void write() {
        // using will close the stream once we finish
        using (FileStream file = new FileStream(_filePath!, FileMode.Create, FileAccess.Write, FileShare.Read)) {
            string str_data = JsonSerializer.Serialize<T>(_values);
            // stream write accepts the array of bytes representing the data
            byte[] bytes = Encoding.UTF8.GetBytes(str_data);
            file.Write(bytes, 0, bytes.Length);
        }
    }

    public void edit(T data) {
        _values = data;
        write();
    }
}