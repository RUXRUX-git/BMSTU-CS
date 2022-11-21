public class Lab2
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

    public static void Main() {
        int n = InputInt("Введите размер первого массива:");
        int m = InputInt("Введите размер второго массива:");

        int[] a = new int[n];
        int[] b = new int[m];
        int[] c = new int[n + m];

        Console.WriteLine("Ввод чисел для первого массива:");
        for (int i = 0; i < n; ++i) {
            a[i] = InputInt($"Введите число под номером {i + 1}:");
            c[i] = a[i];
        }

        Console.WriteLine("Ввод чисел для второго массива:");
        for (int i = 0; i < m; ++i) {
            b[i] = InputInt($"Введите число под номером {i + 1}:");
            c[n + i] = b[i];
        }

        for (int i = 0; i < n; ++i) {
            for (int j = i + 1; j < n; ++j) {
                if (c[j] < c[i]) {
                    int tmp = c[i];
                    c[i] = c[j];
                    c[j] = tmp;
                }
            }
        }

        for (int i = n; i < n + m; ++i) {
            for (int j = i + 1; j < n + m; ++j) {
                if (c[j] > c[i]) {
                    int tmp = c[i];
                    c[i] = c[j];
                    c[j] = tmp;
                }
            }
        }

        Console.WriteLine("Результирующий массив:");
        for (int i = 0; i < n + m; ++i) {
            Console.Write($"{c[i]} ");
            Console.WriteLine();
        }
    }
}