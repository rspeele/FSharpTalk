open System
type User = { FirstName : string; LastName : string }
let users : User list = []

let fullName(user : User) = // explicitly annotated argument type
    user.FirstName + " " + user.LastName

// a function taking an IEnumerable<User>
let showFullNames(users) =
    for user in users do
        // type of user is inferred since
        // we use the fullName function
        Console.WriteLine(fullName(user))

