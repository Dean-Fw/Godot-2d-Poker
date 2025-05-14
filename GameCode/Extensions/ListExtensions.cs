using System.Collections.Generic;

public static class ListExtensions
{
    public static T GetNext<T>(this List<T> input, T current)
    {
        return input.IndexOf(current) + 1 >= input.Count ? input[0] : input[input.IndexOf(current) + 1];
    }
}
