open System

type FizzBuzzer =
    static member FizzBuzz() : unit =
        for i = 0 to 100 do
            let fizzbuzz =
                if i % 15 = 0 then "FizzBuzz"
                elif i % 3 = 0 then "Fizz"
                elif i % 5 = 0 then "Buzz"
                else string i

            Console.WriteLine(fizzbuzz)
