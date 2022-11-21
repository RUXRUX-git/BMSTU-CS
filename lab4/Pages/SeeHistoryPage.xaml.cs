namespace lab4.Pages;

public partial class SeeHistoryPage : ContentPage
{
	public SeeHistoryPage()
	{
		InitializeComponent();
	}

    private void ClearFields()
    {
        clientNumberEntry.Text = "";
        accountNumberEntry.Text = "";
    }

    private async Task<bool> ValidateNumberEntry
    (
    Entry entry,
    List<string> errorMessages,
    bool positive = true
    )
    {
        if (string.IsNullOrEmpty(entry.Text))
        {
            ClearFields();
            await DisplayAlert(
                "Неверный ввод",
                errorMessages[0],
                "OK"
            );
            return false;
        }
        int num;
        if (!int.TryParse(entry.Text, out num))
        {
            ClearFields();
            await DisplayAlert(
                "Неверный ввод",
                errorMessages[1],
                "OK"
            );
            return false;
        }
        if (positive && num < 0)
        {
            ClearFields();
            await DisplayAlert(
                "Неверный ввод",
                errorMessages[2],
                "OK"
            );
            return false;
        }

        return true;
    }

    private async Task<bool> ValidateForm()
    {
        if (!await ValidateNumberEntry(clientNumberEntry,
        new List<string>
        {
            "Номер клиента не может быть пустым",
            "Номер клиента должен быть числом",
            "Номер клиента не может быть отрицательным",
        }
        ))
        {
            return false;
        }
        int clientNum;
        int.TryParse(clientNumberEntry.Text, out clientNum);
        if (clientNum < 1 || clientNum > Global.Clients.Count())
        {
            ClearFields();
            await DisplayAlert(
                "Неверный ввод",
                $"Не существует клиента с номером {clientNum}",
                "OK"
            );
            return false;
        }

        if (!await ValidateNumberEntry(accountNumberEntry,
            new List<string>
            {
                "Номер счета не может быть пустым",
                "Номер счета должен быть числом",
                "Номер счета не может быть отрицательным",
            }
            ))
        {
            return false;
        }
        uint accountNum;
        uint.TryParse(accountNumberEntry.Text, out accountNum);
        if (Global.Clients[clientNum - 1].CanOpenAccount(accountNum))
        {
            ClearFields();
            await DisplayAlert(
                "Неверный ввод",
                "Счета с таким номером не существует",
                "OK"
            );
            return false;
        }

        return true;
    }

    private async void FormSubmitted(object sender, EventArgs e)
    {
        mainTableSection.Clear();

        if (!await ValidateForm())
        {
            return;
        }

        int clientNumber;
        int.TryParse(clientNumberEntry.Text, out clientNumber);
        uint accountNumber;
        uint.TryParse(accountNumberEntry.Text, out accountNumber);
        var history = Global.Clients[clientNumber - 1].ShowHistory(accountNumber);
        if (history.Count() == 0)
        {
            noOperationsLabel.IsVisible = true;
            mainTable.IsVisible = false;
        } else
        {
            for (int i = 0; i < history.Count(); ++i)
            {
                TextCell cell = new TextCell();
                cell.Text = history[i];
                mainTableSection.Add(cell);
            }
            noOperationsLabel.IsVisible = false;
            mainTable.IsVisible = true;
        }
    }

    private void ReturnToMain(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
}
