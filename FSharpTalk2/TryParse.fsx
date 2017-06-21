open System

let inline tryParse(str) =
    let mutable value = Unchecked.defaultof< ^a >
    let success = (^a : (static member TryParse : string * ^a byref -> bool)(str, &value))
    if success then
        Some(value)
    else
        None

let exampleDateTime : DateTime option =
    tryParse "2017-06-21"

let exampleInt : int option =
    tryParse "1" // Some(1)

let exampleShort : int16 option =
    tryParse "2" // Some(2us)
