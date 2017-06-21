module Main
open System
open Formulas.Provider

type MarkupFormula = Formula<"price = cost + cost * markup">

let exampleCost = MarkupFormula.cost(markup = 0.15m, price = 10m)

[<EntryPoint>]
let main argv =
    let exampleCost = MarkupFormula.cost(markup = 0.1m, price = 11m)
    let examplePrice = MarkupFormula.price(markup = 0.5m, cost = 5m)
    let exampleMarkup = MarkupFormula.markup(cost = 10m, price = 20m)
    Console.WriteLine(exampleCost)
    Console.WriteLine(examplePrice)
    Console.WriteLine(exampleMarkup)

    0 // return an integer exit code
