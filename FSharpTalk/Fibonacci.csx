public static class Whatever
{
    private static int Fib(int prev1, int prev2, int countdown)
    {
        if (countdown <= 0)
            return prev2;
        else
            return Fib(prev2, prev1 + prev2, countdown - 1);
    }

    public static int Fibonacci(int n) =>
        Fib(0, 1, n);
}
