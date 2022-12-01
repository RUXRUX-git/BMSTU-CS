using System.Collections.Generic;

public class SymNotFoundException : Exception
{
    public SymNotFoundException()
    {
    }

    public SymNotFoundException(string message)
        : base(message)
    {
    }

    public SymNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class OperationNotFoundException : Exception
{
    public OperationNotFoundException()
    {
    }

    public OperationNotFoundException(string message)
        : base(message)
    {
    }

    public OperationNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class UndefinedSymbolException : Exception
{
    public UndefinedSymbolException()
    {
    }

    public UndefinedSymbolException(string message)
        : base(message)
    {
    }

    public UndefinedSymbolException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class UndefinedActionException : Exception
{
    public UndefinedActionException()
    {
    }

    public UndefinedActionException(string message)
        : base(message)
    {
    }

    public UndefinedActionException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class UndefinedCommandException : Exception
{
    public UndefinedCommandException()
    {
    }

    public UndefinedCommandException(string message)
        : base(message)
    {
    }

    public UndefinedCommandException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

enum Operation
{
    Begin,
    Plus,
    Minus,
    Multiply,
    Divide,
    OpenBracket,
    Exit,
}

enum Action
{
    I,
    II,
    III,
    IV,
    EXIT,
}

public class K
{
    static private List<string> acceptable_types = new List<string>() {
        "+", "-", "*", "/", "(", ")", "number",
    };
    public string type;
    public string value;

    public K(string type_string) {
        if (acceptable_types.Contains(type_string)) {
            type = type_string;
            value = "";
        } else {
            throw new ArgumentException("Unknown type: '" + type_string + "'");
        }
    }

    public K(string type_string, string value_string) {
        if (acceptable_types.Contains(type_string)) {
            type = type_string;
            value = value_string;
        } else {
            throw new ArgumentException("Unknown type: '" + type_string + "'");
        }
    }
}

public class HW1
{
    private static Dictionary<string, Dictionary<Operation, Action>> sym_and_operation_to_action = new Dictionary<string, Dictionary<Operation, Action>>() {
            {"+", new Dictionary<Operation, Action>(){
                {Operation.Begin, Action.I},
                {Operation.Plus, Action.II},
                {Operation.Minus, Action.IV},
                {Operation.Multiply, Action.IV},
                {Operation.Divide, Action.IV},
                {Operation.OpenBracket, Action.I},
            }},
            {"-", new Dictionary<Operation, Action>() {
                {Operation.Begin, Action.I},
                {Operation.Plus, Action.I},
                {Operation.Minus, Action.II},
                {Operation.Multiply, Action.IV},
                {Operation.Divide, Action.IV},
                {Operation.OpenBracket, Action.I},
            }},
            {"*", new Dictionary<Operation, Action>() {
                {Operation.Begin, Action.I},
                {Operation.Plus, Action.I},
                {Operation.Minus, Action.I},
                {Operation.Multiply, Action.II},
                {Operation.Divide, Action.IV},
                {Operation.OpenBracket, Action.I},
            }},
            {"/", new Dictionary<Operation, Action>() {
                {Operation.Begin, Action.I},
                {Operation.Plus, Action.I},
                {Operation.Minus, Action.I},
                {Operation.Multiply, Action.I},
                {Operation.Divide, Action.II},
                {Operation.OpenBracket, Action.I},
            }},
            {"(", new Dictionary<Operation, Action>() {
                {Operation.Begin, Action.I},
                {Operation.Plus, Action.I},
                {Operation.Minus, Action.I},
                {Operation.Multiply, Action.I},
                {Operation.Divide, Action.I},
                {Operation.OpenBracket, Action.I},
            }},
            {")", new Dictionary<Operation, Action>() {
                {Operation.Plus, Action.IV},
                {Operation.Minus, Action.IV},
                {Operation.Multiply, Action.IV},
                {Operation.Divide, Action.IV},
                {Operation.OpenBracket, Action.III},
            }},
            {"", new Dictionary<Operation, Action>() {
                {Operation.Begin, Action.EXIT},
                {Operation.Plus, Action.IV},
                {Operation.Minus, Action.IV},
                {Operation.Multiply, Action.IV},
                {Operation.Divide, Action.IV},
            }},
        };
    private static List<string> digits = new List<string>() {
        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
    };
    private static List<string> op_symbols = new List<string>() {
        "+", "-", "*", "/", "(", ")", "",
    };

    private static Action GetAction(Operation operation, string sym) {
        Dictionary<Operation, Action> operations_to_actions;
        Dictionary<Operation, Action>? tmp_dict;
        if (!sym_and_operation_to_action.TryGetValue(sym, out tmp_dict)) {
            throw new SymNotFoundException("Symbol not found: '" + sym + "'");
        }
        operations_to_actions = tmp_dict;

        Action action;
        if (!operations_to_actions.TryGetValue(operation, out action)) {
            throw new OperationNotFoundException("Operation not found: '" + operation + "'");
        }

        return action;
    }

    private static Operation SymbolToOperation(string sym) {
        if (sym == "+") {
            return Operation.Plus;
        } else if (sym == "-") {
            return Operation.Minus;
        } else if (sym == "*") {
            return Operation.Multiply;
        } else if (sym == "/") {
            return Operation.Divide;
        } else if (sym == "(") {
            return Operation.OpenBracket;
        } else if (sym == "") {
            return Operation.Exit;
        } else {
            throw new UndefinedSymbolException("Undefined symbol: '" + sym + "'");
        }
    }

    private static string OperationToSymbol(Operation operation) {
        if (operation == Operation.Plus) {
            return "+";
        } else if (operation == Operation.Minus) {
            return "-";
        } else if (operation == Operation.Multiply) {
            return "*";
        } else if (operation == Operation.Divide) {
            return "/";
        } else if (operation == Operation.OpenBracket) {
            return "(";
        } else if (operation == Operation.Exit) {
            return "";
        } else {
            throw new OperationNotFoundException("Undefined operation: '" + operation + "'");
        }
    }

    private static List<K> MakeReversePolish(string equation) {
        List<Operation> operations = new List<Operation>(){Operation.Begin};
        List<K> commands = new List<K>();

        int pos = 0;
        string literal = "";
        while (operations.Count() != 0) {
            string sym = "";
            if (pos < equation.Count()) {
                sym = equation[pos].ToString();
            }

            if (digits.Contains(sym)) {
                literal += sym;
                pos++;
            } else if (op_symbols.Contains(sym)) {
                if (literal.Count() != 0) {
                    commands.Add(new K("number", literal));
                    literal = "";
                }

                Action action = GetAction(operations.Last(), sym);
                if (action == Action.I) {
                    Operation operation = SymbolToOperation(sym);
                    operations.Add(operation);
                    pos++;
                } else if (action == Action.II) {
                    Operation operation = SymbolToOperation(sym);
                    operations.Add(operation);
                    pos++;
                } else if (action == Action.III) {
                    operations.RemoveAt(operations.Count() - 1);
                    pos++;
                } else if (action == Action.IV) {
                    string op_symbol = OperationToSymbol(operations.Last());
                    commands.Add(new K(op_symbol));
                    operations.RemoveAt(operations.Count() - 1);
                } else if (action == Action.EXIT) {
                    break;
                } else {
                    throw new UndefinedActionException("Unknown action: '" + action + "'");
                }
            }
        }

        return commands;
    }

    private static double InterpretReversePolish(List<K> commands) {
        List<double> operands = new List<double>();
        while (commands.Count() != 0) {
            if (commands[0].type == "number") {
                operands.Add(int.Parse(commands[0].value));
            } else {
                if (commands[0].type == "+") {
                    operands[operands.Count() - 2] = operands[operands.Count() - 2] + operands[operands.Count() - 1];
                } else if (commands[0].type == "-") {
                    operands[operands.Count() - 2] = operands[operands.Count() - 2] - operands[operands.Count() - 1];
                } else if (commands[0].type == "*") {
                    operands[operands.Count() - 2] = operands[operands.Count() - 2] * operands[operands.Count() - 1];
                } else if (commands[0].type == "/") {
                    if (operands[operands.Count() - 1] == 0) {
                        throw new DivideByZeroException();    
                    }
                    operands[operands.Count() - 2] = operands[operands.Count() - 2] / operands[operands.Count() - 1];
                } else {
                    throw new UndefinedCommandException("Unknown command: " + commands[0]);
                }
                operands.RemoveAt(operands.Count() - 1);
            }
            commands.RemoveAt(0);
        }

        return operands[0];
    }

    private static void Test() {
        string[] inputs = {"749+233", "532-917", "633*109", "213/26", "344*(325+519/595)-558*694+51881"};
        double[] outputs = {749+233, 532-917, 633*109, 213.0/26, 344*(325+519.0/595)-558*694+51881};
        bool errors_met = false;

        for (int i = 0; i < inputs.Count(); ++i) {
            double res = InterpretReversePolish(MakeReversePolish(inputs[i]));
            if (Math.Abs(outputs[i] - res) > 0.0000001) {
                Console.WriteLine("Для ввода '{0}' ожидался вывод {1}, получили {2}", inputs[i], outputs[i], res);
                errors_met = true;
            }
        }

        if (!errors_met) {
            Console.WriteLine("Тесты пройдены успешно");
        } else {
            Console.WriteLine("В тестах обнаружены ошибки");
        }
    }

    public static void Main() {
        Console.WriteLine("Результаты прогона тестов:");
        Test();
        Console.WriteLine("Введите математическое выражение:");
        string eq = Console.ReadLine()!;
        List<K> commands = MakeReversePolish(eq);
        try {
            Console.WriteLine("Результат вычисления: {0}", InterpretReversePolish(commands));
        } catch (DivideByZeroException) {
            Console.WriteLine("Обнаружено деление на 0");
        }
    }
}

