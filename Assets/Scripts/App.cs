using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class App : MonoBehaviour
{
    private const int DELAY = 2000;

    [SerializeField] private Text _textBoard;
    [SerializeField] private InputField _input;
    [SerializeField] private Button _enterTextButton;

    private Task _lastTask = Task.CompletedTask;
    private Reader _reader;
    private Writer _writer;
    private string _text;

    private void Awake()
    {
        var path = Application.dataPath + "/filenok";
        Debug.Log(path);

        _reader = new Reader(path);
        _writer = new Writer(path);

        Task.Run(WhileReading);

        _enterTextButton.onClick.AddListener(() =>
        {
            _lastTask =_lastTask.ContinueWith((task) => Write(_input.text));
        });
    }

    private void Update()
    {
        _textBoard.text = _text;
    }

    private async Task Write(string text)
    {
        await _writer.Write(text);
    }

    private async Task WhileReading()
    {
        while (true)
        {
            await Task.Delay(DELAY);
            Debug.Log("Hello");

            _lastTask = _lastTask.ContinueWith((task) =>
            {
                _text = _reader.Read().Result;
            });
        }
    }
}