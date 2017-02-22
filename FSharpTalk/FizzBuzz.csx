using System;
using System.Collections.Generic;
using System.Globalization;

public static class FizzBuzzer
{
    public static void FizzBuzz()
    {
        for (var i = 0; i <= 100; i++)
        {
            var fizzbuzz =
                i % 15 == 0 ? "FizzBuzz"
                : i % 3 == 0 ? "Fizz"
                : i % 5 == 0 ? "Buzz"
                : i.ToString();

            Console.WriteLine(fizzbuzz);
        }
    }
}