namespace lab4;

static class Global
{
    private static List<BankClient> clients = new List<BankClient>
    {
        new BankClient("Мышкин Феликс Эдуардович", 36, "Школа"),
        new BankClient("Носков Авраам Николаевич", 43, "ВУЗ"),
        new BankClient("Рогов Владимир Митрофанович", 51, "Автомойка"),
    };

	public static List<BankClient> Clients
    {
		get { return clients; }
		set { clients = value; }
    }
}

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();
	}

    private void ShowSeeUsersPage(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new Pages.SeeUsersPage());
    }

    private void ShowAddUserPage(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new Pages.AddUserPage());
    }

	private void ShowAddAccountPage(object sender, EventArgs e)
    {
		App.Current.MainPage = new NavigationPage(new Pages.AddAccountPage());
    }

    private void ShowDeleteAccountPage(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new Pages.DeleteAccountPage());
    }

    private void ShowAddMoneyPage(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new Pages.AddMoneyPage());
    }

    private void ShowTakeMoneyPage(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new Pages.TakeMoneyPage());
    }

    private void ShowSeeBalancePage(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new Pages.SeeBalancePage());
    }

    private void ShowSeeHistoryPage(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new Pages.SeeHistoryPage());
    }
}


