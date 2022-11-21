enum Command
{
    Add = 1,
    Substract,
    Multiple,
    Divide,
}

public class SomeClass
{
    private static int InputInt(string prompt = "Введите число:", string errMessage = "Вы ввели не число, попробуйте еще раз:") {
        if (!string.IsNullOrEmpty(prompt)) {
            Console.WriteLine(prompt);
        }

        int num;
        bool ok = int.TryParse(Console.ReadLine(), out num);
        while (!ok) {
            Console.WriteLine(errMessage);
            ok = int.TryParse(Console.ReadLine(), out num);
        }
        return num;
    }

    private static double InputDouble(string prompt = "Введите число:", string errMessage = "Вы ввели не число, попробуйте еще раз:") {
        if (!string.IsNullOrEmpty(prompt)) {
            Console.WriteLine(prompt);
        }

        double num;
        bool ok = double.TryParse(Console.ReadLine(), out num);
        while (!ok) {
            Console.WriteLine(errMessage);
            ok = double.TryParse(Console.ReadLine(), out num);
        }
        return num;
    }

    private static bool CommandValid(int command) {
        IEnumerable<Command> allCommands = Enum.GetValues(typeof(Command)).Cast<Command>();
        int minCommand = ((int)allCommands.Min());
        int maxCommand = ((int)allCommands.Max());
        if (command >= minCommand && command <= maxCommand) {
            return true;
        } else {
            return false;
        }
    }

    static Command InputCommand(string prompt = "Введите номер команды:",
                                string numErrMessage = "Вы ввели не число, попробуйте еще раз:",
                                string commandErrMessage = "Вы ввели некорректный номер команды, попробуйте еще раз:") {
        int commandNum = InputInt(prompt, numErrMessage);
        bool ok = CommandValid(commandNum);
        while (!ok) {
            Console.WriteLine(commandErrMessage);
            commandNum = InputInt(prompt, numErrMessage);
            ok = CommandValid(commandNum);
        }
        return (Command)commandNum;
    }

    static double PerformCommand(Command command, double left, double right) {
        switch (command)
        {
            case Command.Add:
                return left + right;
            case Command.Substract:
                return left - right;
            case Command.Multiple:
                return left * right;
            case Command.Divide:
                return left / right;
            default:
                Console.WriteLine($"Неизвестная команда под номером {command}");
                return double.NaN;
        }
    }
    
    static void DescribeCalculator() {
        Console.WriteLine("Эта программа - калькулятор");
        Console.WriteLine("Сначала необходимо ввести номер команды:");
        Console.WriteLine("1 - команда '+'");
        Console.WriteLine("2 - команда '-'");
        Console.WriteLine("3 - команда '*'");
        Console.WriteLine("4 - команда '/'");
        Console.WriteLine("Затем необходимо ввести первый операнд - число с плавающей запятой");
        Console.WriteLine("Затем необходимо ввести второй операнд - число с плавающей запятой");
        Console.WriteLine("Если все данные введены верно, то программа выведет результат операции.");

        Console.WriteLine();
    }

    public static void Main() {
        DescribeCalculator();

        while (true) {
            Command command = InputCommand();
            double firstOperand = InputDouble("Введите первый операнд:", "Неверный формат числа - правильный формат <>,<>");
            double secondOperand = InputDouble("Введите второй операнд:", "Неверный формат числа - правильный формат <>,<>");

            while (command == Command.Divide && Math.Abs(secondOperand) < double.Epsilon) {
                Console.WriteLine("Деление на 0! Попробуйте заново.");
                command = InputCommand();
                firstOperand = InputDouble("Введите первый операнд:");
                secondOperand = InputDouble("Введите второй операнд:");
            }

            double res = PerformCommand(command, firstOperand, secondOperand);
            Console.WriteLine($"Результат операции: {res}");
        }
    }
}