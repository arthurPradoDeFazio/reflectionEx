using System.Reflection;

namespace reflectionEx;
class Program
{
    public static void GetPropertiesAndMethods<T>(Task obj)
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

    private static string GetSignature(MethodInfo method)
    {
        System.Text.StringBuilder signature = new System.Text.StringBuilder();

        signature.Append(method.IsPublic ? "public" : "private");
        signature.Append(method.IsStatic ? " static" : "");
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
        signature.Remove(signature.Length - 1, 1);
        signature.Append(')');

        return signature.ToString();
    }

}

