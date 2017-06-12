open System
open System.IO

type ConsoleWorkflow<'a> =
    | Question of question : string * onAnswer : (string -> ConsoleWorkflow<'a>)
    | Done of 'a

let ask question =
    Question (question, fun answer -> Done answer)

type ConsoleWorkflowBuilder() =
    member this.Return(x) = Done x
    member this.Bind(question : ConsoleWorkflow<'a>, onAnswer : 'a -> ConsoleWorkflow<'b>) =
        match question with
        | Done answer -> onAnswer answer
        | Question (question, innerOnAnswer) ->
            Question (question, fun answer -> this.Bind(innerOnAnswer answer, onAnswer))

let console = ConsoleWorkflowBuilder()

let exampleWorkflow =
    console {
        let! shapeType = ask "What kind of shape is it? Circle or rectangle?"
        match shapeType.ToUpperInvariant() with
        | "CIRCLE" ->
            let! radius = ask "What's the radius?"
            let radius = Double.Parse(radius)
            return radius * radius * Math.PI
        | "RECTANGLE" ->
            let! width = ask "What's the width?"
            let! height = ask "What's the height?"
            return Double.Parse(width) * Double.Parse(height)
        | _ ->
            return raise (InvalidDataException("Don't know that shape, sorry"))
    }

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
