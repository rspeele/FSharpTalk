/// C# style POCO -- like class with { get; set; } properties
type PointPoco() =
    member val X = 0 with get, set
    member val Y = 0 with get, set

let examplePointPoco1 = new PointPoco(X = 2, Y = 4)
let examplePointPoco2 =
    new PointPoco
        ( X = 3
        , Y = 9
        )

// F# immutable record -- like a super-tuple
type PointRecord =
    {
        X : int
        Y : int
    } // automatically has Equals, GetHashCode, CompareTo

let examplePointRecord1 = { X = 3; Y = 9 }
let examplePointRecord2 =
    {
        X = 2
        Y = 4
    }
let examplePointRecord3 = { examplePointRecord2 with Y = 1 }

// Custom getters/setters for if you actually have logic to run
type PointPocoCustomGetSet() =
    let mutable x = 0
    let mutable y = 0
    member this.X
        with get() = x
        and set(value) =
            printfn "setting x to %d" value
            x <- value
    member this.Y 
        with get() = y
        and set(value) =
            printfn "setting y to %d" value
            y <- value