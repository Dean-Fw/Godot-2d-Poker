using System.Collections.Generic;

public static class ListExtensions
{
    public static T GetNext<T>(this List<T> input, int currentIndex)
    {
        return currentIndex + 1 >= input.Count ? input[0] : input[currentIndex + 1];
    }
}
