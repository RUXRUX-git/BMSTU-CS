class BankClient 
{
    public string fullName;
    public uint age;
    public string workPlace;
    public List<BankAccount> accounts;

    public BankClient(string fullName, uint age, string workPlace) {
        this.fullName = fullName;
        this.age = age;
        this.workPlace = workPlace;
        this.accounts = new List<BankAccount>();
    }

    private int FindAccount(uint number) {
        int pos = -1;
        for (int i = 0; i < accounts.Count(); ++i) {
            if (accounts[i].number == number) {
                pos = i;
                break;
            }
        }
        return pos;
    }

    public void OpenAccount(uint number) {
        if (FindAccount(number) != -1) {
            Console.WriteLine("Счет уже существует");
            return;
        }
        accounts.Add(new BankAccount(number));
        Console.WriteLine("Счет успешно открыт");
    }

    public void CloseAccount(uint number) {
        int pos = FindAccount(number);
        if (pos == -1) {
            Console.WriteLine("Счет не найден");
            return;
        }
        if (accounts[pos].CanClose()) {
            accounts.RemoveAt(pos);
            Console.WriteLine("Счет успешно закрыт");
            return;
        }
        Console.WriteLine("Невозможно закрыть счет - перед этим надо вывести с него все деньги");
    }

    public void AddMoney(uint accountNumber, uint amount) {
        int pos = FindAccount(accountNumber);
        if (pos == -1) {
            Console.WriteLine("Счет не найден");
            return;
        }

        accounts[pos].AddMoney(amount);
        Console.WriteLine("Деньги успешно начислены");
    }

    public void TakeMoney(uint accountNumber, uint amount) {
        int pos = FindAccount(accountNumber);
        if (pos == -1) {
            Console.WriteLine("Счет не найден");
            return;
        }

        if (accounts[pos].TakeMoney(amount)) {
            Console.WriteLine("Деньги успешно сняты");
        } else {
            Console.WriteLine("На счете недостаточно средств");
        }
    }

    public void ShowBalance(uint accountNumber) {
        int pos = FindAccount(accountNumber);
        if (pos == -1) {
            Console.WriteLine("Счет не найден");
            return;
        }
        Console.WriteLine($"Баланс: {accounts[pos].Balance}");
    }

    public void ShowHistory(uint accountNumber) {
        int pos = FindAccount(accountNumber);
        if (pos == -1) {
            Console.WriteLine("Счет не найден");
            return;
        }

        accounts[pos].ShowHistory();
    }
}

class BankAccount
{
    public uint number;
    public List<int> history;
    private uint balance;
    public uint Balance {
        get { return balance; }
    }

    public BankAccount(uint number) {
        this.number = number;
        this.history = new List<int>();
        this.balance = 0;
    }

    public void AddMoney(uint amount) {
        history.Add((int)amount);
        balance += amount;
    }
    public bool TakeMoney(uint amount) {
        if (amount > balance) {
            return false;
        }
        history.Add(-(int)amount);
        balance -= amount;
        return true;
    }
    public void ShowHistory() {
        Console.WriteLine($"Информация по счету {number}:");
        if (!history.Any()) {
            Console.WriteLine("Нет операций по данному счету");
        } else {
            for (int i = 0; i < history.Count(); ++i) {
                if (history[i] > 0) {
                    Console.WriteLine($"Пополнение {history[i]}");
                } else {
                    Console.WriteLine($"Снятие {-history[i]}");
                }
            }
        }
    }
    public bool CanClose() {
        if (balance == 0) {
            return true;
        } else { 
            return false;
        }
    }
}

class Lab3
{
    private static int InputInt(string prompt = "Введите число:", 
                                string errMessage = "Вы ввели не число, попробуйте еще раз:") 
    {
        if (!string.IsNullOrEmpty(prompt)) {
            Console.WriteLine(prompt);
        }
        
        int num;
        while (!int.TryParse(Console.ReadLine(), out num)) {
            Console.WriteLine(errMessage);
        }
        return num;
    }

    private static int[] InputIntArr(string numPrompt = "Введите число:",
                                     string arrPrompt = "Введите длину массива",
                                     string errMessage = "Вы ввели не число, попробуйте еще раз:") {
        uint arrLen = InputUint(arrPrompt);
        int[] res = new int[arrLen];
        for (int i = 0; i < arrLen; ++i) {
            Console.WriteLine($"Номер {i + 1}:");
            res[i] = InputInt(numPrompt, errMessage);
        }

        return res;
    }

    private static uint InputUint(string prompt = "Введите число:", 
                                string errMessage = "Вы ввели не число, попробуйте еще раз:") 
    {
        if (!string.IsNullOrEmpty(prompt)) {
            Console.WriteLine(prompt);
        }
        
        uint num;
        while (!uint.TryParse(Console.ReadLine(), out num)) {
            Console.WriteLine(errMessage);
        }
        return num;
    }

    private static uint[] InputUintArr(string numPrompt = "Введите число:",
                                       string arrPrompt = "Введите длину массива:",
                                       string errMessage = "Вы ввели не число, попробуйте еще раз:") {
        uint arrLen = InputUint(arrPrompt);
        uint[] res = new uint[arrLen];
        for (int i = 0; i < arrLen; ++i) {
            Console.WriteLine($"Номер {i + 1}:");
            res[i] = InputUint(numPrompt, errMessage);
        }

        return res;
    }

    public static void ParseInput(string input) {

    }

    private static void ShowClients(List<BankClient> clients) {
        if (!clients.Any()) {
            Console.WriteLine("Список клиентов банка пуст");
            return;
        }
        Console.WriteLine("Список клиентов банка:");
        for (int i = 0; i < clients.Count(); ++i) {
            Console.WriteLine($"Клиент номер {i + 1}:");
            Console.WriteLine($"Имя: {clients[i].fullName}");
            Console.WriteLine($"Возраст: {clients[i].age}");
            Console.WriteLine($"Место работы: {clients[i].workPlace}");
        }
    }

    private static void ShowOperationList() {
        Console.WriteLine("Список операций:");
        Console.WriteLine("1 - открыть счет");
        Console.WriteLine("2 - закрыть счет");
        Console.WriteLine("3 - вложить деньги");
        Console.WriteLine("4 - снять деньги");
        Console.WriteLine("5 - посмотреть баланс");
        Console.WriteLine("6 - посмотреть историю");
    }

    private static void OpenAccount(ref List<BankClient> clients, int clientNumber, uint accountNumber) {
        clients[clientNumber].OpenAccount(accountNumber);
    }

    private static void CloseAccount(ref List<BankClient> clients, int clientNumber, uint accountNumber) {
        clients[clientNumber].CloseAccount(accountNumber);
    }

    private static void AddMoney(ref List<BankClient> clients, int clientNumber, uint accountNumber) {
        uint amount = InputUint("Введите сумму:");
        clients[clientNumber].AddMoney(accountNumber, amount);
    }

    private static void TakeMoney(ref List<BankClient> clients, int clientNumber, uint accountNumber) {
        uint amount = InputUint("Введите сумму:");
        clients[clientNumber].TakeMoney(accountNumber, amount);
    }

    private static void ShowBalance(ref List<BankClient> clients, int clientNumber, uint accountNumber) {
        clients[clientNumber].ShowBalance(accountNumber);
    }

    private static void ShowHistory(ref List<BankClient> clients, int clientNumber, uint accountNumber) {
        clients[clientNumber].ShowHistory(accountNumber);
    }

    private static void PerformOperation(ref List<BankClient> clients, int clientNumber) {
        clientNumber -= 1;

        if (clientNumber < 0 || clientNumber >= clients.Count()) {
            Console.WriteLine("Неверный номер клиента");
            return;
        }

        int opNum = InputInt("Введите номер операции:");
        while (opNum < 1 || opNum > 6) {
            Console.WriteLine("Неверный номер операции. Попробуйте еще раз");
            opNum = InputInt("Введите номер операции:");
        }

        uint accountNumber = InputUint("Введите номер счета:");

        switch (opNum) {
            case 1:
                OpenAccount(ref clients, clientNumber, accountNumber);
                break;
            case 2:
                CloseAccount(ref clients, clientNumber, accountNumber);
                break;
            case 3:
                AddMoney(ref clients, clientNumber, accountNumber);
                break;
            case 4:
                TakeMoney(ref clients, clientNumber, accountNumber);
                break;
            case 5:
                ShowBalance(ref clients, clientNumber, accountNumber);
                break;
            case 6:
                ShowHistory(ref clients, clientNumber, accountNumber);
                break;
        }
    }



    public static void Main() {
        List<BankClient> clients = new List<BankClient>();
        clients.Add(new BankClient(
            "Мышкин Феликс Эдуардович", 36, "Школа"
        ));
        clients.Add(new BankClient(
            "Носков Авраам Николаевич", 43, "ВУЗ"
        ));
        clients.Add(new BankClient(
            "Рогов Владимир Митрофанович", 51, "Автомойка"
        ));

        ShowClients(clients);
        Console.WriteLine();
        ShowOperationList();
        Console.WriteLine();

        while (true) {
            int clientNumber = InputInt("Выберите номер клиента, для которого производить операцию:");
            PerformOperation(ref clients, clientNumber);
        }
    }
}