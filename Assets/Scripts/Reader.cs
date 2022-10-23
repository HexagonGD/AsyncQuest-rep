using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class Reader
{
    private readonly string _path;
    public string Text { get; private set; }

    public Reader(string path)
    {
        _path = path;
    }

    public async Task Read()
    {
        using (var stream = new StreamReader(_path))
        {
            Text = await stream.ReadToEndAsync();
        }
    }
}