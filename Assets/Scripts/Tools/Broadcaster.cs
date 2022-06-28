using System;

public static class Broadcaster
{
    public static void Send(Action broadcast)
    {
        if (broadcast != null)
            broadcast();
    }

    public static void Send<T>(Action<T> broadcast, T input)
    {
        if (broadcast != null)
            broadcast(input);
    }

    public static void Send<T, U>(Action<T, U> broadcast, T input1, U input2)
    {
        if (broadcast != null)
            broadcast(input1, input2);
    }

    public static void Send<T, U, V>(Action<T, U, V> broadcast, T input1, U input2, V input3)
    {
        if (broadcast != null)
            broadcast(input1, input2, input3);
    }

    public static void Send<T, U, V, W>(Action<T, U, V, W> broadcast, T input1, U input2, V input3, W input4)
    {
        if (broadcast != null)
            broadcast(input1, input2, input3, input4);
    }

}
