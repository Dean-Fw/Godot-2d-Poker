using System.Linq;
using Godot;

public static class ContainerExtensions
{
    public static bool ContainsChildOfType<T>(this Container container)
    {
        return container.GetChildren().ToList().Exists(c => c is T);
    }

    public static void Clear(this Container container)
    {
        foreach (var child in container.GetChildren())
        {
            child.QueueFree();
        }
    }
}
