using System;
namespace lab4;

class AccountAlreadyExistException : Exception
{
}

class AccountNotFoundException : Exception
{
}

class NotZeroAccountBalanceException : Exception
{
}

class AccountBalanceNotEnoughException : Exception
{
}

public class BankClient
{
    public string fullName;
    public uint age;
    public string workPlace;
    public List<BankAccount> accounts;

    public BankClient(string fullName, uint age, string workPlace)
    {
        this.fullName = fullName;
        this.age = age;
        this.workPlace = workPlace;
        this.accounts = new List<BankAccount>();
    }

    private int FindAccount(uint number)
    {
        int pos = -1;
        for (int i = 0; i < accounts.Count(); ++i)
        {
            if (accounts[i].number == number)
            {
                pos = i;
                break;
            }
        }
        return pos;
    }

    public void OpenAccount(uint number)
    {
        if (!CanOpenAccount(number))
        {
            throw new AccountAlreadyExistException();
        }
        accounts.Add(new BankAccount(number));
    }

    public bool CanOpenAccount(uint number)
    {
        if (FindAccount(number) != -1)
        {
            return false;
        }
        return true;
    }

    public void CloseAccount(uint number)
    {
        int pos = FindAccount(number);
        if (pos == -1)
        {
            throw new AccountNotFoundException();
        }
        if (accounts[pos].CanClose())
        {
            accounts.RemoveAt(pos);
            return;
        }
        throw new NotZeroAccountBalanceException();
    }

    public void AddMoney(uint accountNumber, uint amount)
    {
        int pos = FindAccount(accountNumber);
        if (pos == -1)
        {
            throw new AccountNotFoundException();
        }

        accounts[pos].AddMoney(amount);
    }

    public bool CanTake(uint accountNumber, uint amount)
    {
        int pos = FindAccount(accountNumber);
        if (pos == -1)
        {
            return false;
        }

        if (!accounts[pos].CanTake(amount))
        {
            return false;
        }

        return true;
    }

    public void TakeMoney(uint accountNumber, uint amount)
    {
        int pos = FindAccount(accountNumber);
        if (pos == -1)
        {
            throw new AccountNotFoundException();
        }

        if (!accounts[pos].TakeMoney(amount))
        {
            throw new AccountBalanceNotEnoughException();
        }
    }

    public uint GetBalance(uint accountNumber)
    {
        int pos = FindAccount(accountNumber);
        if (pos == -1)
        {
            throw new AccountNotFoundException();
        }
        return accounts[pos].Balance;
    }

    public List<string> ShowHistory(uint accountNumber)
    {
        int pos = FindAccount(accountNumber);
        if (pos == -1)
        {
            throw new AccountNotFoundException();
        }

        return accounts[pos].ShowHistory();
    }
}

public class BankAccount
{
    public uint number;
    public List<int> history;
    private uint balance;
    public uint Balance
    {
        get { return balance; }
    }

    public BankAccount(uint number)
    {
        this.number = number;
        this.history = new List<int>();
        this.balance = 0;
    }

    public void AddMoney(uint amount)
    {
        history.Add((int)amount);
        balance += amount;
    }
    public bool CanTake(uint amount)
    {
        if (amount > balance)
        {
            return false;
        }
        return true;
    }
    public bool TakeMoney(uint amount)
    {
        if (amount > balance)
        {
            return false;
        }
        history.Add(-(int)amount);
        balance -= amount;
        return true;
    }
    public List<String> ShowHistory()
    {
        List<string> res = new List<string>();

        //Console.WriteLine($"Информация по счету {number}:");
        if (!history.Any())
        {
            res.Add("Нет операций по данному счету");
        }
        else
        {
            for (int i = 0; i < history.Count(); ++i)
            {
                if (history[i] > 0)
                {
                    res.Add($"Пополнение {history[i]}");
                }
                else
                {
                    res.Add($"Снятие {-history[i]}");
                }
            }
        }

        return res;
    }
    public bool CanClose()
    {
        if (balance == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
