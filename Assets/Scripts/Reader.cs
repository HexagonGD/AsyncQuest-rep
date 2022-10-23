using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class Reader
{
    private readonly string _path;

    public Reader(string path)
    {
        _path = path;
    }

    public async Task<string> Read()
    {
        string text;
        using (var stream = new StreamReader(_path))
        {
            text = await stream.ReadToEndAsync();
        }
        Debug.Log("Read");
        return text;
    }
}