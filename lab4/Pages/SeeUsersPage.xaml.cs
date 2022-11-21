namespace lab4.Pages;

public partial class SeeUsersPage : ContentPage
{
	public SeeUsersPage()
	{
		InitializeComponent();

		if (Global.Clients.Count() == 0)
        {
			mainLayout.Remove(mainTable);
			Label label = new Label();
			label.Text = "Нет клиентов";
			label.FontSize = 18;
			label.HorizontalOptions = LayoutOptions.Center;
			mainLayout.Insert(0, label);
        }

		for (int i = 0; i < Global.Clients.Count(); ++i)
        {
			BankClient client = Global.Clients[i];
            TextCell cell = new TextCell();
			List<uint> accounts = new List<uint>();
			for (int j = 0; j < client.accounts.Count(); ++j)
            {
				accounts.Add(client.accounts[j].number);
            }
			string accountsStr;
			if (accounts.Count() == 0)
            {
				accountsStr = "нет счетов";
            } else
            {
				accountsStr = String.Join(", ", accounts.ToArray());
            }
			cell.Text = $"{i + 1}. {client.fullName}, " +
                $"{client.age} {Plural(client.age)}";
			cell.Detail = $"Работа: {client.workPlace}, счета: {accountsStr}";

			mainTableSection.Add(cell);
        }
	}

	private static List<string> declension = new List<string>
	{
		"год",
		"года",
		"лет",
	};


    private static string Plural(int number, List<string> titles)
    {
		List<int> cases = new List<int> { 2, 0, 1, 1, 1, 2 };
		return titles[(number % 100 > 4 && number % 100 < 20) ? 2 : cases[(number % 10 < 5) ? number % 10 : 5]];
    }

    private static string Plural(int number)
    {
		return Plural(number, declension);
    }

    private static string Plural(uint number)
    {
        return Plural((int)number);
    }

    private void ReturnToMain(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
}
