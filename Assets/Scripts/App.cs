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

    private void Awake()
    {
        var path = Application.dataPath + "/filenok";
        Debug.Log(path);

        _reader = new Reader(path);
        _writer = new Writer(path);

        Task.Run(() => UnityMainThreadDispatcher.Instance().EnqueueAsync(WhileReading));

        _enterTextButton.onClick.AddListener(() =>
        {
            _lastTask = _lastTask.ContinueWith((task) => Write(_input.text));
        });
    }

    private async Task Write(string text)
    {
        await _writer.Write(text);
    }

    private async void WhileReading()
    {
        while (true)
        {
            await Task.Delay(DELAY);
            _lastTask = _lastTask.ContinueWith((task) => _reader.Read());
            _lastTask.Wait();
            _textBoard.text = _reader.Text;
        }
    }
}