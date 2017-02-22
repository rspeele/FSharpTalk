open System
open System.Collections.Generic

// Pattern matching can be used with more than discriminated unions!

/// As a super-powered switch statement:
let nameOfNumber n =
    match n with
    | neg when neg < 0 -> "something negative"
    | 0 -> "zero"
    | 1 -> "one"
    | 2 -> "two"
    | _ -> "some other number" // _ is a wildcard

/// Less bug-prone runtime type tests (this is a reimplementation of LINQ's .Count() extension)
let count (xs : IEnumerable<'a>) =
    match xs with
    | :? IReadOnlyCollection<'a> as readOnly ->
        readOnly.Count
    | :? ICollection<'a> as collection ->
        collection.Count
    | _ -> // looks like we have to iterate to count it
        let mutable count = 0
        for x in xs do
            count <- count + 1
        count

/// Working with tuples:
let fizzbuzz() =
    for i = 0 to 100 do
        let choice =
            match (i % 3, i % 5) with
            | (0, 0) -> "FizzBuzz"
            | (0, _) -> "Fizz"
            | (_, 0) -> "Buzz"
            | _ -> string i
        Console.WriteLine(choice)

/// Active patterns
let (|HasValue|Null|) (x : Nullable<'a>) =
    if x.HasValue then HasValue x.Value
    else Null

let doSomething (x : Nullable<int>) =
    match x with
    | HasValue v -> Console.WriteLine("It's not null, it's " + string v)
    | Null -> Console.WriteLine("Oh, it's null")