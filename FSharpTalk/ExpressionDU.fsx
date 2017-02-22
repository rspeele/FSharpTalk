type Expression =
    | Literal of int
    | Addition of Expression * Expression
    | Multiplication of Expression * Expression
    // etc. for each type of expression we have

// each module would normally be in a separate file or even separate assembly
module Formatter =
    let rec format (expr : Expression) =
        match expr with
        | Literal x ->
            string x
        | Addition (left, right) ->
            sprintf "(%s + %s)" (format left) (format right)
        | Multiplication (left, right) ->
            sprintf "(%s + %s)" (format left) (format right)

module Evaluator =
    let rec evaluate (expr : Expression) =
        match expr with
        | Literal x -> x
        | Addition (left, right) -> evaluate left + evaluate right
        | Multiplication (left, right) -> evaluate left * evaluate right

// etc for other concerns such as...
// static type-checking
// syntax highlighting
// providing Resharper-style refactoring
// optimizing at the expression level
// compiling to IL