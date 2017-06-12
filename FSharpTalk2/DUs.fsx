open System

(*

The first thing I really like about F# is how easy it is to model plain old data.

Plain old data is inescapable, because all useful systems ultimately process it and output it.
Information a user types into a form is plain old data. Information displayed to the user is plain old data.

When I say plain old data, I mean information directly related to the domain you're working in.
These types don't have private members because they don't have implementation details, so there is nothing to hide.

*)

type ExpenseLineItemDetail =
    {   Cost : decimal
    }

type ServiceLineItemDetail =
    {   BillableRate : decimal
        HoursBilled : decimal
    }

type InvoiceLineItemDetail =
    | Expense of ExpenseLineItemDetail
    | Service of ServiceLineItemDetail

type InvoiceLineItem =
    {   Description : string
        DollarAmount : decimal
        Detail : InvoiceLineItemDetail
    }

