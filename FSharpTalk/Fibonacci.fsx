module Whatever =
    let fibonacci n =
        let rec fib previous current countdown =
            if countdown <= 0 then
                current
            else
                fib current (previous + current) (countdown - 1)
        fib 0 1 n

open Whatever

let example = fibonacci 5
// example = 8