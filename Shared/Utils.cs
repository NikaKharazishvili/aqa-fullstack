namespace Shared;

public static class Utils
{
    // Simple print method, similar to Python's print(). Allows usage like: print("Hello", "World") → prints "Hello World"
    // We use 'params' so we can write: print("a", 1, true) instead of: print(new object[] { "a", 1, true })
    public static void Print(params object[] objects) => Console.WriteLine(string.Join(" ", objects));

    // Pause for 'seconds'
    public static void Wait(float seconds) => Thread.Sleep((int)(seconds * 1000));
}