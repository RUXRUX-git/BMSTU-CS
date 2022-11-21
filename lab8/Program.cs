public class Lab8
{
    private static int maxLen = 300;
    public static void Main() {
        Console.WriteLine("Программа для деления вещественных чисел");
        while(true) {
            Console.WriteLine();
            Console.WriteLine("Введите первое число:");
            string? num1Str = Console.ReadLine();

            if (string.IsNullOrEmpty(num1Str)) {
                Console.WriteLine("Не введено число");
                continue;
            }

            if (num1Str.Length > maxLen) {
                Console.WriteLine("Введено слишком длинное число");
                continue;
            }

            decimal num1;
            try {
                num1 = Convert.ToDecimal(num1Str);
            } catch (FormatException) {
                Console.WriteLine("Ошибка преобразования");
                continue;
            }


            Console.WriteLine("Введите второе число:");
            string? num2Str = Console.ReadLine();

            if (string.IsNullOrEmpty(num2Str)) {
                Console.WriteLine("Не введено число");
                continue;
            }

            if (num2Str.Length > maxLen) {
                Console.WriteLine("Введено слишком длинное число");
                continue;
            }

            decimal num2;
            try {
                num2 = Convert.ToDecimal(num2Str);
            } catch (FormatException) {
                Console.WriteLine("Ошибка преобразования");
                continue;
            }
            

            decimal res;
            try {
                res = num1 / num2;
            } catch (DivideByZeroException) {
                Console.WriteLine("Ошибка: деление на 0");
                continue;
            }

            Console.WriteLine($"Результат деления: {res}");
        }
    }
}