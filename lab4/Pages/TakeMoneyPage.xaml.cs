namespace lab4.Pages;

public partial class TakeMoneyPage : ContentPage
{
	public TakeMoneyPage()
	{
		InitializeComponent();
	}

    private void ClearFields()
    {
        clientNumberEntry.Text = "";
        accountNumberEntry.Text = "";
        amountEntry.Text = "";
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

        if (!await ValidateNumberEntry(amountEntry, new List<string>
        {
            "Количество не может быть пустым",
            "Количество должно быть числом",
            "Количество не должно быть отрицательным",
        }))
        {
            return false;
        }
        uint amount;
        uint.TryParse(amountEntry.Text, out amount);
        if (Global.Clients[clientNum - 1].CanTake(accountNum, amount))
        {
            ClearFields();
            await DisplayAlert(
                "Неверный ввод",
                $"Невозможно снять сумму ({amount}) со счета: недостаточно средств",
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

        int clientNumber;
        int.TryParse(clientNumberEntry.Text, out clientNumber);
        uint accountNumber;
        uint.TryParse(accountNumberEntry.Text, out accountNumber);
        uint amount;
        uint.TryParse(amountEntry.Text, out amount);
        Global.Clients[clientNumber - 1].TakeMoney(accountNumber, amount);
        ClearFields();
        await DisplayAlert(
            "Деньги успешно сняты",
            "",
            "OK"
        );
    }


        private void ReturnToMain(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
}
