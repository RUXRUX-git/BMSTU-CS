namespace lab4.Pages;

public partial class AddUserPage : ContentPage
{
	public AddUserPage()
	{
		InitializeComponent();
	}

	private void ClearFields()
    {
        fioEntry.Text = "";
        ageEntry.Text = "";
        workPlaceEntry.Text = "";
    }

    private async Task<bool> ValidateForm()
    {
        if (string.IsNullOrEmpty(fioEntry.Text))
        {
            ClearFields();
            await DisplayAlert(
                "Неверный ввод",
                "ФИО не может быть пустым",
                "OK"
            );
            return false;
        }

        if (string.IsNullOrEmpty(ageEntry.Text))
        {
            ClearFields();
            await DisplayAlert(
                "Неверный ввод",
                "Возраст не может быть пустым",
                "OK"
            );
            return false;
        }
        int num;
        if (!int.TryParse(ageEntry.Text, out num))
        {
            ClearFields();
            await DisplayAlert(
                "Неверный ввод",
                "Возраст должен быть числом",
                "OK"
            );
            return false;
        }
        if (num < 0)
        {
            ClearFields();
            await DisplayAlert(
                "Неверный ввод",
                "Возраст не может быть отрицательным",
                "OK"
            );
            return false;
        }

        if (string.IsNullOrEmpty(workPlaceEntry.Text))
        {
            ClearFields();
            await DisplayAlert(
                "Неверный ввод",
                "Место работы не может быть пустым",
                "OK"
            );
            return false;
        }

        return true;
    }

	private async void FormSubmitted(object sender, EventArgs e)
    {
		if (!await ValidateForm())
        {
            return;
        }

        string fio = fioEntry.Text;
        uint age;
        uint.TryParse(ageEntry.Text, out age);
        string workPlace = workPlaceEntry.Text;

        Global.Clients.Add(new BankClient(fio, age, workPlace));
        ClearFields();
        await DisplayAlert(
            $"Пользователь с именем {fio} успешно добавлен\n" +
            $"Ему присвоен номер {Global.Clients.Count()}",
            "",
            "OK"
        );
    }

    private void ReturnToMain(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
}
