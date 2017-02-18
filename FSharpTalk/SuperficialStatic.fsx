open System
open System.Collections.Generic

type InterviewQuestions =
    static member FizzBuzz() : unit =
        for i = 0 to 100 do
            let fizzbuzz =
                if i % 15 = 0 then "FizzBuzz"
                elif i % 3 = 0 then "Fizz"
                elif i % 5 = 0 then "Buzz"
                else string i

            Console.WriteLine(fizzbuzz)

    static member IterativeFibonaccis() : IEnumerable<int> =
        seq {
            let mutable prev1 = 0
            let mutable prev2 = 1
            while true do
                yield prev2
                let next = prev1 + prev2
                prev1 <- prev2
                prev2 <- next
        }

    static member RecursiveFibonaccis(prev1, prev2) : IEnumerable<int> =
        seq {
            let next = prev1 + prev2
            yield next
            yield! InterviewQuestions.RecursiveFibonaccis(prev2, next)
        }
    static member RecursiveFibonaccis() : IEnumerable<int> =
        InterviewQuestions.RecursiveFibonaccis(0, 1)