// This type is in the F# standard library, defined like so:
// type option<'a> =
//     | Some of 'a
//     | None

let safeDivide (numerator : int) (denominator : int) : option<int> =
    if denominator = 0 then None
    else Some (numerator / denominator)

let run () =
    match safeDivide 16 3 with
    | Some x -> printfn "That's a number! %d" x
    | None -> printfn "Nope, can't get a meaningful answer for this"

    // Why is this good?
    // 1. Can't get "x" out without pattern matching.
    // 2. Can't get "x" out in the None branch.
    // 3. In the Some branch, we have "x" unwrapped as an int, not an option<int>

    let defaultToNegative1 : int =
        match safeDivide 5 0 with
        | Some x -> x
        | None -> -1

    printfn "%d" defaultToNegative1