using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class Writer
{
    private readonly string _path;

    public Writer(string path)
    {
        _path = path;
    }

    public async Task Write(string str)
    {
        using (var stream = new StreamWriter(_path, true))
        {
            await stream.WriteLineAsync(str);
            await stream.FlushAsync();
        }
        Debug.Log("Write");
    }
}