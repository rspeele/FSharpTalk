#I "../packages/FSharp.Data.2.3.2/lib/net40"
#r "FSharp.Data.DesignTime.dll"
#r "FSharp.Data.dll"

open FSharp.Data
open System
open System.IO

// Generate type at compile time from the HTML on Wikipedia
type CSharpArticle = HtmlProvider<"https://en.wikipedia.org/wiki/C_Sharp_(programming_language)">

let run () =
    // Load the article at runtime using the generated type
    let page = CSharpArticle.Load("https://en.wikipedia.org/wiki/C_Sharp_(programming_language)")
    // Scrape stuff out of the HTML in a type-safe way
    let versionsTable = page.Tables.Versions
    for row in versionsTable.Rows do
        let date = row.Date // datetime
        let version = row.Version // string
        let net = row.``.NET Framework`` // string
        printfn "%O %s %s" date version net

// This is brittle -- if they significantly change the way the page is structured, our program will break.
// But that is the nature of scraping, and at least we will find out when we build instead of when we run.