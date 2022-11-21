public class Lab2_2 
{
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

    private static void ShowMatrix(int[][] A) {
        for (int i = 0; i < A.Count(); ++i) {
            for (int j = 0; j < A[i].Count(); ++j) {
                Console.Write($"{A[i][j]} ");
            }
            Console.WriteLine();
        }
    }

    public static void Main() {
        uint rowNum = InputUint("Введите количество строк:");
        uint colNum = InputUint("Введите количество столбцов:");

        Random rnd = new Random();

        int[][] A = new int[rowNum][];
        int[][] A_rows = new int[rowNum][];
        int[][] A_cols = new int[rowNum][];
        for (int i = 0; i < rowNum; ++i) {
            A[i] = new int[colNum];
            A_rows[i] = new int[colNum];
            A_cols[i] = new int[colNum];
            for (int j = 0; j < colNum; ++j) {
                A[i][j] = rnd.Next() % 10;
                A_rows[i][j] = A[i][j];
                A_cols[i][j] = A[i][j];
            }
        }

        Console.WriteLine("Исходная матрица:");
        ShowMatrix(A);

        for (int i = 0; i < rowNum; ++i) {
            for (int j = 0; j < colNum; ++j) {
                for (int k = j + 1; k < colNum; ++k) {
                    if (A_rows[i][k] < A_rows[i][j]) {
                        int tmp = A_rows[i][k];
                        A_rows[i][k] = A_rows[i][j];
                        A_rows[i][j] = tmp;
                    }
                }
            }
        }
        Console.WriteLine("Матрица, отсортированная по строкам:");
        ShowMatrix(A_rows);

        for (int i = 0; i < colNum; ++i) {
            for (int j = 0; j < rowNum; ++j) {
                for (int k = j + 1; k < rowNum; ++k) {
                    if (A_cols[k][i] < A_cols[j][i]) {
                        int tmp = A_cols[k][i];
                        A_cols[k][i] = A_cols[j][i];
                        A_cols[j][i] = tmp;
                    }
                }
            }
        }
        Console.WriteLine("Матрица, отсортированная по столбцам:");
        ShowMatrix(A_cols);
    }
}