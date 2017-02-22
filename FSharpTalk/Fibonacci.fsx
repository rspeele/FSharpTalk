module Whatever =
    let fibonacci n =
        let rec fib prev1 prev2 countdown =
            if countdown <= 0 then
                prev2
            else
                fib prev2 (prev1 + prev2) (countdown - 1)
        fib 0 1 n

open Whatever

let example = fibonacci 5
// example = 8