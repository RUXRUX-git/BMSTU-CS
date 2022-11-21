namespace lab5;

using System.Collections.ObjectModel;
using System.Diagnostics;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Word> Words { get; set; }

    public MainPage()
	{
		InitializeComponent();
        Words = new ObservableCollection<Word>();
        this.BindingContext = this;
	}

	private async void ReadFileClicked(object sender, EventArgs e)
    {
		var txtFileType = new FilePickerFileType(
			new Dictionary<DevicePlatform, IEnumerable<string>>
			{
                { DevicePlatform.WinUI, new[] {".txt"} },
                { DevicePlatform.macOS, new[] {"txt"} },
				{ DevicePlatform.MacCatalyst, new[] {"txt"} },
			});
        PickOptions options = new()
        {
            FileTypes = txtFileType,
        };
        List<string> uniqueWords = new List<string>();
        string[] lines;
        Stopwatch stopWatch = new Stopwatch();
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            stopWatch.Start();
            using (var stream = System.IO.File.OpenRead(result.FullPath))
            using (var reader = new StreamReader(stream))
            {
                string fileText = reader.ReadToEnd();
                lines = fileText.Split(
                    new string[] { Environment.NewLine },
                    StringSplitOptions.None
                );
            }
            stopWatch.Stop();
        }
        catch (Exception ex)
        {
			await DisplayAlert(
				"Произошла ошибка",
				ex.Message,
				"ОК"
				);
            return;
        }

        TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        await DisplayAlert(
            "Время",
            elapsedTime,
            "ОК"
            );


        for (int i = 0; i < lines.Length; ++i)
        {
            string[] words = lines[i].Split(" ");
            for (int j = 0; j < words.Length; ++j)
            {
                if (!uniqueWords.Contains(words[j]))
                {
                    uniqueWords.Add(words[j]);
                }
            }
        }
        await DisplayAlert(
            "Содержимое",
            String.Join(" ", uniqueWords.ToArray()),
            "ОК"
            );

        Words.Clear();
        for (int i = 0; i < uniqueWords.Count(); ++i)
        {
            Words.Add(new Word(uniqueWords[i]) );
        }

    }
}

public class Word
{
    public string Content { get; set; }
    public Word(string word)
    {
        Content = word;
    }
}

