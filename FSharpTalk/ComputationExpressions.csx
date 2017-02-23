using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class ComputationExpressionsDemo
{
    // special kind of method for lazy sequence generation in C#:
    // returns IEnumerable<T> and uses "yield return" instead of "return".
    public static IEnumerable<int> Fibonaccis()
    {
        var current = 0;
        var next = 1;
        while (true)
        {
            yield return current;
            var sum = current + next;
            current = next;
            next = sum;
        }
    }

    // special kind of method for asynchronous code in C#:
    // returns Task<T> and uses "async" keyword in declaration, "await" in body.
    public static async Task<int> AsyncCopyFile()
    {
        using (var input = File.OpenRead("example.csv"))
        using (var output = File.OpenRead("copy.csv"))
        {
            var reading = true;
            var totalBytesCopied = 0;
            var buffer = new byte[0x1000];
            while (reading)
            {
                // await blocks this code till the end of the async operation
                // but while it's waiting, the thread can be used by other code
                var countRead = await input.ReadAsync(buffer, 0, buffer.Length);

                await output.WriteAsync(buffer, 0, countRead);

                reading = countRead > 0;
                totalBytesCopied = totalBytesCopied + countRead;
            }
        }
    }
}