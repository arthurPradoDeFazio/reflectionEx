using System.Reflection;

namespace reflectionEx;
class Program
{
    public static void GetPropertiesAndMethods<T>(T obj)
    {
        var type = obj.GetType();

        Console.WriteLine("Propriedades:");
        foreach (var property in type.GetProperties())
            Console.WriteLine($"{property.GetType().Name} {property.Name}");

        Console.WriteLine();

        Console.WriteLine("Métodos: ");
        foreach (var method in type.GetMethods())
            Console.WriteLine(GetSignature(method));
    }

    private static string GetSignature(MethodInfo method) // fonte: https://stackoverflow.com/questions/2267277/get-private-properties-method-of-base-class-with-reflection
    {
        System.Text.StringBuilder signature = new System.Text.StringBuilder();

        signature.Append(method.IsPublic ? "public" : "private");
        signature.Append(method.IsStatic ? " static" : "");
        signature.Append($" {method.ReturnType.Name}");
        signature.Append($"  {method.Name}");

        if (method.IsGenericMethod)
        {
            signature.Append('<');
            foreach (var genericArgument in method.GetGenericArguments())
                signature.Append($"{genericArgument.Name}, ");
            signature.Remove(signature.Length - 1, 1);
            signature.Append('>');
        }

        signature.Append('(');
        foreach (var argument in method.GetParameters())
            signature.Append($"{argument.GetType().Name} {argument.Name},");
        if (signature[signature.Length - 1] != '(')
            signature.Remove(signature.Length - 1, 1);
        signature.Append(')');

        return signature.ToString();
    }

    public static T? getObj<T>(T obj) where T: class

    {
        if (obj == null)
            return null as T;

        foreach (var c in obj.GetType().GetConstructors())
        {
            var l = c.GetParameters();
            if (l.GetLength(0) == 0)
            {
                return (T)c.Invoke(new object[] { });
            }
        }

        return null as T;
    }

    public static void Main()
    {
        string x = "bla";
        GetPropertiesAndMethods(x);
        string y = getObj(x);
        Console.WriteLine($"y = {y}");
    }

}

