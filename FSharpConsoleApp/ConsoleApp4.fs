open CSharpConsoleApp3
open System

type WorkflowBuilder() =
    member this.Bind(first : Workflow<'a>, next : 'a -> Workflow<'b>) =
        first.AndThen(fun a -> next(a))

    member this.Return(x) =
        Workflow.Done(x)

let flow = WorkflowBuilder() // instantiate it

let getAreaDesugared() =
    flow.Bind(Workflow.Ask("What shape is it?"), fun shape ->
        match shape.ToLowerInvariant() with
        | "rectangle" ->
            let questions =
                Workflow
                    .Ask("How wide is the rectangle?")
                    .Also(Workflow.Ask("How tall is the rectangle?"))
            flow.Bind(questions, fun struct(width, height) ->
                flow.Return(Double.Parse(width) * Double.Parse(height))
            )
        | "circle" ->
            flow.Bind(Workflow.Ask("What is the circle's radius?"), fun radiusInput ->
                let radius = Double.Parse(radiusInput)
                flow.Return(radius * radius * Math.PI)
            )
        | _ ->
            flow.Return(failwith("Unsupported shape"))
    )

let getArea() =
    flow {
        let! shape = Workflow.Ask("What shape is it?")
        match shape.ToLowerInvariant() with
        | "rectangle" ->
            let! struct(width, height) =
                Workflow
                    .Ask("How wide is the rectangle?")
                    .Also(Workflow.Ask("How tall is the rectangle?"))

            return Double.Parse(width) * Double.Parse(height)

        | "circle" ->
            let! radiusInput = Workflow.Ask("What is the circle's radius?")
            let radius = Double.Parse(radiusInput)

            return radius * radius * Math.PI

        | _ ->
            return failwith("Unsupported shape")
    }

[<STAThread>]
[<EntryPoint>]
let main argv =
    let asker = FormQuestionAsker()
    try
        let area = getArea().Run(asker)
        Console.WriteLine("Result: " + string(area))
    with
    | exn ->
        Console.Error.WriteLine(exn.Message)
    ignore(Console.ReadLine())
    0 // return an integer exit code
