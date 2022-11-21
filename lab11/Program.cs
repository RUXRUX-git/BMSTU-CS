class Point
{
    public double X { get; }
    public double Y { get; }

    public Point(double x, double y) {
        X = x;
        Y = y;
    }

    override public string ToString() {
        return $"({X.ToString("0.000")},{Y.ToString("0.000")})";
    }

    public static List<Point> RandomSet(int count) {
        DateTime dateTime = DateTime.Now;
        Random rnd = new Random((int)dateTime.TimeOfDay.TotalMilliseconds);
        List<Point> res = new List<Point>();
        for (int i = 0; i < count; ++i) {
            res.Add(new Point(rnd.NextDouble(), rnd.NextDouble()));
        }

        return res;
    }
}

class SortByCoordinateStart : IComparer<Point>
{
    int IComparer<Point>.Compare(Point? a, Point? b) {
        if (a == null || b == null) {
            throw new NullReferenceException("can't compare null Point");
        }

        double aDist = Math.Sqrt(a.X * a.X + a.Y * a.Y);
        double bDist = Math.Sqrt(b.X * b.X + b.Y * b.Y);
        if (aDist > bDist) {
            return 1;
        } else if (aDist == bDist) {
            return 0;
        } else {
            return -1;
        }
    }
}

class SortByXAxis : IComparer<Point>
{
    int IComparer<Point>.Compare(Point? a, Point? b) {
        if (a == null || b == null) {
            throw new NullReferenceException("can't compare null Point");
        }

        if (a.Y > b.Y) {
            return 1;
        } else if (a.Y == b.Y) {
            return 0;
        } else {
            return -1;
        }
    }
}

class SortByYAxis : IComparer<Point>
{
    int IComparer<Point>.Compare(Point? a, Point? b) {
        if (a == null || b == null) {
            throw new NullReferenceException("can't compare null Point");
        }

        if (a.X > b.X) {
            return 1;
        } else if (a.X == b.X) {
            return 0;
        } else {
            return -1;
        }
    }
}

class SortByDiagonal : IComparer<Point>
{
    // Высота (проведенная из прямого угла) в прямоугольном треугольнике равна
    // a * b / c
    // Тут катеты равны, a = a => c = sqrt(2*a*a) = sqrt(2)*a
    // Значит, высота равна a * a / (sqrt(2)*a) = a / sqrt(2)
    // a / sqrt(2) > b / sqrt(2) <=> a > b, если a, b >= 0, значит, можно не делить
    int IComparer<Point>.Compare(Point? a, Point? b) {
        if (a == null || b == null) {
            throw new NullReferenceException("can't compare null Point");
        }

        double aSide = Math.Abs(a.X - a.Y);
        double bSide = Math.Abs(b.X - b.Y);

        if (aSide > bSide) {
            return 1;
        } else if (aSide == bSide) {
            return 0;
        } else {
            return -1;
        }
    }
}

class Lab11
{
    private static int InputInt(string prompt = "Введите число:", 
                                string errMessage = "Вы ввели не число, попробуйте еще раз") 
    {
        Console.WriteLine(prompt);
        int num;
        while (!int.TryParse(Console.ReadLine(), out num)) {
            Console.WriteLine(errMessage);
            Console.WriteLine(prompt);
        }
        return num;
    }
    private static void ShowPointList(ref List<Point> points) {
        Console.WriteLine("{" + string.Join("; ", points) + "}");
    }
    public static void Main() {
        int pointCount = InputInt("Введите количество точек:");
        List<Point> points = Point.RandomSet(pointCount);

        Console.WriteLine("Исходный массив:");
        ShowPointList(ref points);

        Console.WriteLine("Массив, отсортированный по удалению от начала координат:");
        SortByCoordinateStart sbcs = new SortByCoordinateStart();
        points.Sort(sbcs);
        ShowPointList(ref points);

        Console.WriteLine("Массив, отсортированный по удалению от оси абсцисс:");
        SortByXAxis sbxa = new SortByXAxis();
        points.Sort(sbxa);
        ShowPointList(ref points);

        Console.WriteLine("Массив, отсортированный по удалению от оси ординат:");
        SortByYAxis sbya = new SortByYAxis();
        points.Sort(sbya);
        ShowPointList(ref points);

        Console.WriteLine("Массив, отсортированный по удалению от прямой y=x:");
        SortByDiagonal sbd = new SortByDiagonal();
        points.Sort(sbd);
        ShowPointList(ref points);
    }
}