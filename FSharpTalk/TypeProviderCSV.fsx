#I "../packages/FSharp.Data.2.3.2/lib/net40"
#r "FSharp.Data.DesignTime.dll"
#r "FSharp.Data.dll"

open FSharp.Data
open System
open System.IO

// Generate a type at compile time to represent Yahoo stock files. The sample data
// the type provider looks at to generate the type is a CSV file I obtained from:
//      http://ichart.finance.yahoo.com/table.csv?s=MSFT
type Stocks = CsvProvider<"sample.csv">

let run () =
    let exampleText =
        """Date,Open,High,Low,Close,Volume,Adj Close
    2017-02-21,64.610001,64.949997,64.449997,64.489998,19384900,64.489998
    2017-02-17,64.470001,64.690002,64.300003,64.620003,21234600,64.620003
    2017-02-16,64.739998,65.239998,64.440002,64.519997,20524700,64.519997"""
    let stocks = Stocks.Load(new StringReader(exampleText)) // parse CSV file at runtime using generated type

    for row in stocks.Rows do
        let date = row.Date // access parsed data in a statically typed way (this is a DateTime)
        let high = row.High
        printfn "%d %f" date.Day high