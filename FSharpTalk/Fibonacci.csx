public static class Whatever
{
    private static int Fib(int previous, int current, int countdown)
    {
        if (countdown <= 0)
            return current;
        else
            return Fib(current, previous + current, countdown - 1);
    }

    public static int Fibonacci(int n) =>
        Fib(0, 1, n);
}
