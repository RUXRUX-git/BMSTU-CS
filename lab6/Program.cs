delegate int MyDelegate(int par1, string par2, double par3);

class Lab6
{
    public static int MyMethod(int par1, string par2, double par3) {
        return 0;
    }
    public static void SomeOtherMethod(int filler, MyDelegate del) {
        Console.WriteLine(del(filler, "Hello, world!", 3.1415926));
    }
    public static void OneMoreMethod(int filler, Func<int, string, double, int> del) {
        Console.WriteLine(del(filler, "Hello, world!", 3.1415926));
    }
    public static void Main() {
        SomeOtherMethod(0, MyMethod);
        SomeOtherMethod(-1, (int a, string b, double c) => -1);

        OneMoreMethod(0, MyMethod);
        OneMoreMethod(-1, (int a, string b, double c) => -1);
    }
}