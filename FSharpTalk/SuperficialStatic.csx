using System;
using System.Collections.Generic;
using System.Globalization;

public static class InterviewQuestions
{
    public static void FizzBuzz()
    {
        for (var i = 0; i <= 100; i++)
        {
            var fizzbuzz =
                i % 15 == 0 ? "FizzBuzz"
                : i % 3 == 0 ? "Fizz"
                : i % 5 == 0 ? "Buzz"
                : i.ToString(CultureInfo.InvariantCulture);

            Console.WriteLine(fizzbuzz);
        }
    }

    public static IEnumerable<int> IterativeFibonaccis()
    {
        var prev1 = 0;
        var prev2 = 1;
        while (true)
        {
            yield return prev2;
            var next = prev1 + prev2;
            prev1 = prev2;
            prev2 = next;
        }
    }

    public static IEnumerable<int> RecursiveFibonaccis
        (int prev1, int prev2)
    {
        var next = prev1 + prev2;
        yield return next;
        foreach (var more in RecursiveFibonaccis(prev2, next))
            yield return more;
    }
    public static IEnumerable<int> RecursiveFibonaccis()
        => RecursiveFibonaccis(0, 1);
}