open System
type User = { Name : string }
let users : User list = []

for user in users do
	// these lines are in the loop
    Console.WriteLine(user.Name)
    Console.WriteLine("^ that's a user")
Console.WriteLine("loop done")