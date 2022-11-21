class NoughtsAndCrosses
{
    private static int FIELD_SIZE = 3; 
    private List<List<int>> field;  // 0 - пустое поле, 1 - крестик, -1 - нолик

    public NoughtsAndCrosses() {
        field = new List<List<int>>(FIELD_SIZE);
        for (int i = 0; i < FIELD_SIZE; ++i) {
            field.Add(new List<int>(FIELD_SIZE));
            for (int j = 0; j < FIELD_SIZE; ++j) {
                field[i].Add(0);  // в начале все поля пустые
            }
        }
    }

    public static void Describe() {
        Console.WriteLine("Игра крестики-нолики");
        Console.WriteLine("Пример ввода:");
        Console.WriteLine("Поле:");
        Console.WriteLine("   123");
        Console.WriteLine("A     ");
        Console.WriteLine("B     ");
        Console.WriteLine("C     ");
        Console.WriteLine("-->A1");
        Console.WriteLine("Поле:");
        Console.WriteLine("   123");
        Console.WriteLine("A  x  ");
        Console.WriteLine("B     ");
        Console.WriteLine("C     ");
        Console.WriteLine("-->B2");
        Console.WriteLine("Поле:");
        Console.WriteLine("   123");
        Console.WriteLine("A  x  ");
        Console.WriteLine("B   o ");
        Console.WriteLine("C     ");
        Console.WriteLine("--------------------------");
    }

    public bool ParseAndProcessInput(string? input, bool isNought) {
        Console.WriteLine($"input: '{input}'");
        if (input == null || input.Count() < 2) {
            return false;
        }
        int rowNumber = input[0] - 'A' + 1;
        int colNumber = input[1] - '0';
        if (rowNumber < 1 || rowNumber > FIELD_SIZE || colNumber < 1 || colNumber > FIELD_SIZE) {
            return false;
        }

        if (field[rowNumber - 1][colNumber - 1] != 0) {
            return false;
        }

        if (isNought) {
            field[rowNumber - 1][colNumber - 1] = 1;
        } else {
            field[rowNumber - 1][colNumber - 1] = -1;
        }
        return true;
    }

    public int CheckWinner() {
        List<List<int>> rows = new List<List<int>>();
        List<int> acsDiagonal = new List<int>();
        List<int> descDiagonal = new List<int>();
        for (int i = 0; i < FIELD_SIZE; ++i) {
            rows.Add(new List<int>());
            for (int j = 0; j < FIELD_SIZE; ++j) {
                rows[i].Add(field[j][i]);
            }

            if (field[i].TrueForAll((int x) => x == 1) || rows[i].TrueForAll((int x) => x == 1)) {
                return 1;
            } else if (field[i].TrueForAll((int x) => x == -1) || rows[i].TrueForAll((int x) => x == -1)) {
                return -1;
            }

            acsDiagonal.Add(field[i][i]);
            descDiagonal.Add(field[i][FIELD_SIZE - 1 - i]);
        }

        // Console.WriteLine($"acsDiagonal: {string.Join(" ", acsDiagonal)}");
        // Console.WriteLine($"descDiagonal: {string.Join(" ", descDiagonal)}");

        if (acsDiagonal.TrueForAll((int x) => x == 1) || descDiagonal.TrueForAll((int x) => x == 1)) {
            return 1;
        } else if (acsDiagonal.TrueForAll((int x) => x == -1) || descDiagonal.TrueForAll((int x) => x == -1)) {
            return -1;
        }


        return 0;
    }

    public bool AvailableMovesExist() {
        bool exist = false;
        for (int i = 0; i < FIELD_SIZE; ++i) {
            if (field[i].Any((int x) => x == 0)) {
                exist = true;
            };
        }
        return exist;
    }

    public void ShowField() {
        Console.Write("   ");
        for (int i = 0; i < FIELD_SIZE; ++i) {
            Console.Write(i + 1);
        }
        Console.WriteLine();
        Console.WriteLine();
        for (int i = 0; i < FIELD_SIZE; ++i) {
            Console.Write($"{(char)((int)'A' + i)}  ");
            for (int j = 0; j < FIELD_SIZE; ++j) {
                char sym = 'E';
                switch (field[i][j]) {
                    case -1: 
                        sym = 'o';
                        break;
                    case 0:
                        sym = '_';
                        break;
                    case 1:
                        sym = 'x';
                        break;
                }
                Console.Write(sym);
            }
            Console.WriteLine();
        }
    }

}

class Lab12
{

    public static void Main() {
        NoughtsAndCrosses.Describe();
        while (true) {
            NoughtsAndCrosses game = new NoughtsAndCrosses();
        
            int winner = 0;
            bool availableMovesExist = true;
            bool isNought = true;
            while (winner == 0 && availableMovesExist) {
                Console.WriteLine("Поле:");
                game.ShowField();

                if (isNought) {
                    Console.WriteLine("Введите поле, куда поставить крестик:");
                } else {
                    Console.WriteLine("Введите поле, куда поставить нолик:");
                }
                string? input = Console.ReadLine();
                bool ok = game.ParseAndProcessInput(input, isNought);
                while (!ok) {
                    Console.WriteLine("Неверный ввод, попробуйте еще раз:");
                    if (isNought) {
                        Console.WriteLine("Введите поле, куда поставить крестик:");
                    } else {
                        Console.WriteLine("Введите поле, куда поставить нолик:");
                    }
                    input = Console.ReadLine();
                    ok = game.ParseAndProcessInput(input, isNought);
                }

                winner = game.CheckWinner();
                availableMovesExist = game.AvailableMovesExist();
                isNought = !isNought;
            }

            if (!availableMovesExist) {
                Console.WriteLine("Ничья");
            } else if (winner == 1) {
                Console.WriteLine("Победил первый игрок");
            } else {
                Console.WriteLine("Победил второй игрок");
            }
            game.ShowField();
        }        
    }

}


