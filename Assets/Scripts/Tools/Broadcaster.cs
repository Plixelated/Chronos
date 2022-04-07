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

}
