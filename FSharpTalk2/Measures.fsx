open System

[<Measure>]
type dollar

let myFirstDollar : int<dollar> = 1<dollar>

[<Measure>]
type hour =
    static member OfTimeSpan(timeSpan : TimeSpan) =
        decimal(timeSpan.TotalHours) * 1m<hour>

type Contractor =
    {
        Name : string
        BillingRate : decimal<dollar/hour>
    }

let exampleCostCalculation : decimal<dollar> =
    let contractor =
        {
            Name = "Graham"
            BillingRate = 140m<dollar/hour>
        }
    let hours = hour.OfTimeSpan(DateTime.UtcNow - DateTime(2017, 6, 20))
    let cost = contractor.BillingRate * hours
    cost