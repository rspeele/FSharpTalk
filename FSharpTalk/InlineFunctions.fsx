open System

// You can declare inline functions with "let inline".
let inline square x =
    x * x

let a = square 2.0 // 4.0, type float
let b = square 2   // 4, type int

// C# equivalent would be to have overloads for each type you support -- see Math.Abs for example

// The same feature can be used to write functions that work on any type with a given method or set of methods.
// Use sparingly -- this can get confusing.

/// Works with any type that has a method like:
///     bool MyType.TryParse(string input, out MyType output)
let inline tryParse (str : string) =
    let mutable result = Unchecked.defaultof< ^a > // null for reference types, 0 for int, 0.0 for float, etc.
    let success = (^a : (static member TryParse : string * ^a byref -> bool)(str, &result))
    if success then
        Some result
    else
        None

let one : option<int> =
    tryParse "1"
    // Some 1

let failedInt : option<int> =
    tryParse "one"
    // None

let wednesday : option<DateTime> =
    tryParse "2017-02-22"
    // Some 2/22/2017 12:00:00 AM