open System
open System.IO

/// Lazy sequence of all fibonacci numbers
let fibonaccis =
    seq { // Built-in computation expression for lazy IEnumerables
        let mutable current = 0
        let mutable next = 1
        while true do
            yield current
            let sum = current + next
            current <- next
            next <- sum
    }

// Other built-in computation expressions include:
// query (for LINQ-to-whatever)
// async (for asynchronous workflows

let asyncCopyFile() =
    async {
        use input = File.OpenRead("example.csv")
        use output = File.Create("copy.csv")
        let mutable reading = true
        let mutable totalBytesCopied = 0
        let buffer = Array.zeroCreate 0x1000
        while reading do
            // let! blocks this code till the end of the async operation
            // but while it's waiting, the thread can be used by other code
            let! countRead = input.AsyncRead(buffer, 0, buffer.Length)

            // do! X is the same as let! __ignoredVariable = X
            do! output.AsyncWrite(buffer, 0, countRead)

            reading <- countRead > 0
            totalBytesCopied <- totalBytesCopied + countRead
        return totalBytesCopied
    }

/// Option-chaining -- bail out whenever we hit a None
type OptionBuilder() =
    member this.Bind(variableValue : option<'a>, restOfBlock : 'a -> option<'b>) =
        match variableValue with
        | None -> None
        | Some x -> restOfBlock x
    member this.Return(x : 'a) : option<'a> =
        Some x

let option = OptionBuilder()

let mightWork() =
    Console.WriteLine("Type something (or don't):")
    let input = Console.ReadLine()
    if String.IsNullOrEmpty(input) then None
    else Some input

let exampleWorkflow() =
    let maybeResult =
        option {
            let! first = mightWork()
            let! second = mightWork()
            return first + second
        }
    match maybeResult with
    | None -> "Bailed out"
    | Some result -> "Got final result " + result