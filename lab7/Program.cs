public class AttributeClass : System.Attribute
{

}

public class ClassWithInfo
{
    public int myField;
    public int myField2;
    public int MyField {
        get { return myField; }
    }
    [AttributeClass]
    public int MyField2 {
        get { return myField; }
    }
    public ClassWithInfo() {
        this.myField = 0;
    }
    public ClassWithInfo(int val) {
        this.myField = val;
    }
    public static void SayHello() {
        Console.WriteLine("Hello, world!");
    }
    public static void SayGoodbye() {
        Console.WriteLine("Goodbye, world!");
    }
}

public class Lab7
{
    public static void Main() {
        System.Type info = typeof(ClassWithInfo);

        Console.WriteLine("Info about constructors:");
        System.Reflection.ConstructorInfo[] constructorInfo = info.GetConstructors();
        for (int i = 0; i < constructorInfo.Count(); ++i) {
            Console.WriteLine(constructorInfo[i].ToString());
        }
        Console.WriteLine();

        Console.WriteLine("Info about properties:");
        System.Reflection.PropertyInfo[] propertyInfo = info.GetProperties();
        for (int i = 0; i < propertyInfo.Count(); ++i) {
            Console.WriteLine(propertyInfo[i].ToString());
        }
        Console.WriteLine();

        Console.WriteLine("Info about methods:");
        System.Reflection.MethodInfo[] methodInfo = info.GetMethods();
        for (int i = 0; i < methodInfo.Count(); ++i) {
            Console.WriteLine(methodInfo[i].ToString());
        }
        Console.WriteLine();

        Console.WriteLine("Свойства с назначенным атрибутом:");
        foreach (System.Reflection.PropertyInfo prop in info.GetProperties()) {
            object[] attributes = prop.GetCustomAttributes(typeof(AttributeClass), true);
            if (attributes.Length == 1) {
                Console.WriteLine(prop.ToString());
            }
        }
        Console.WriteLine();

        Console.WriteLine("Вызов метода с использованием рефлексии:");
        Type type = Type.GetType("ClassWithInfo")!;
        Object obj = Activator.CreateInstance(type)!;
        System.Reflection.MethodInfo method = type!.GetMethod("SayHello")!;
        method.Invoke(obj, null);
    }
}