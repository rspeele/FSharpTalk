open System
open System.IO

Console.WriteLine(
    let mutable x = 0
    while x  * 2 < 1000 do
        x <- x * 2
    x)

// what is the type of x?
// a variable can't be void, can it?
let x = Console.WriteLine("hello world")

// F# models void with a type called "unit"
// which has only one value

let (y : unit) = () // the "unit" value

// Makes a lot of sense for generics:
// avoids Task<T> vs Task, Func<T> vs Action, etc.
// in favor of Task<unit>, Func<unit>

