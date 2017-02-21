# What is F#?

F# is yet another .NET language, like C# or VB.NET.
It was born in Microsoft Research, a Microsoft rehabilitation facility for academics.

It's a functional programming language, which is a sort of vaguely defined term, but essentially just means
its heritage is with ML, Haskell, Lisp, and so on, rather than C, ALGOL, Fortran, Java. So just like how in C#
we inherit a lot of syntax and concepts from C, F# gets a lot of its ideas and syntax from ML. I mention this as
a sort of apology: if the syntax looks unfamiliar, it's because it was made to be familiar to somebody else, not
just to mess with you.

* Cue: show timeline?

It's slightly younger than C#, but not much -- it was released in 2005, 3 years after C#.
Just like with C#, the language has evolved and typically each new VS version comes with updates to F#.
In the past 12 years, there's been a slow but steady flow of features that were introduced in F# and later
added to C#.

## How much of a hassle is it to use?

Because it compiles to regular old .NET dlls, it's very easy to add a project using F# to an existing solution file,
use classes from your C# projects in the F# project, and vice-versa.

You don't have to go all-in on one language or the other, and I think you probably shouldn't,
since they each have their strengths.

## What are you going to talk about?

I'm not a fan of surprises, so here's the outline up front.

1. **(It's a language)**  Overview of some language differences you have to be aware of.
2. **(It's the best thing since sliced bread)**  The features that really make F# worth using.
3. **(Well... then again)**  Good reasons why *not* to use F# for some projects.
4. **(But seriously, it's good)**  Examples of great libraries written in or designed to work best with F#.

# Part 1: It's a programming language

Whenever you learn a new programming language, there are two parts to the learning process.

The fun part is learning the new concepts. If you're going from say, C to C#, and you've never heard of object-oriented
programming before, you're going to learn about interfaces, inheritance, polymorphism, and a whole host of other things
that don't have a 1-1 correspondence to stuff you wrote in C.

The boring part is learning the superficial differences: how do I do the same stuff I do in other languages, in this one?
Writing functions, if/else, loops, the usual stuff.

I don't want to waste your time talking about those superficial differences, so I'm not going to cover them exhaustively.
If you determine F# is worth learning, you can figure out the details on your own later. However, I do need to talk
about a few things just so you know the score when you read a snippet of F# code.
This part is going to take about 15 minutes, then we'll get to the fun stuff.

## Code comparison

Ok, I'm going to pull up our first code example here. This is an implementation of our favorite interview question,
FizzBuzz, in both F# and C#. Hopefully y'all can tell which is which.

* Cue: show first FizzBuzz (let = if...)

### Whitespace sensitive

The most prominent difference you'll notice is that F# is a whitespace sensitive language.
If you've worked with Python, you'll be familiar with this.
What it means is that instead of using curly braces to delimit blocks of code, the blocks are implied by the indentation.

Some people really like this. Other people really, *really* hate it. Me, I write a lot of C#, but my first language
was Python, so I'm used to both ways and don't think it makes much difference. Either way you get used to it after
spending a few days with a language.

### Everything is an expression

This isn't a big deal, but it is kind of nice In C, C#, Java, and pals, you've got expressions and statements.
They're different things, which is why in our C# FizzBuzz we've got this funky ternary operator here. In C# you couldn't
put an if/else there because if is a statement, not an expression.

* Cue: show broken C# version with if?

In F# everything is an expression. So the "if/elif/else" we used when assigning this variable here is the very same
"if/elif/else" you use as a control structure. Here's another way of writing both:

* Cue: show second FizzBuzz (let mutable = ...; if...)

By the way, you'll also notice from this example that in F#, variable assignments are immutable by default.
We have to be explicit about it if we want to permit reassigning the value of a variable after it's been declared, and
there is a completely different operator for reassignment. I think this is a good design decision, but it's not a very
important one, so let's move on.

One thing you might be wondering is, if everything is an expression, what happens with void functions?
In C# you can't call a void function and pass that into another function, because it doesn't return anything.
void isn't a real type, you can't have a value of type void.

F# has a type called "unit" that is like void, but it is a real type that can have a value. The value is just not
interesting to anybody because it can't be anything except this pair of parentheses with nothing between them,
so there's no reason you'd ever care to look at it. You can't get any information out of that.

So in F#, you *can* pass a "unit" value to another function, you just might not have much reason to do so. The compiler
also politely interops with other .NET languages, so your F# functions returning "unit" look like void functions to
other .NET languages and vice versa.

### Full type inference

* Cue: show C# code example

In C#, for the past 10 years, we've have this keyword "var".
When you declare a local variable you can be really explicit and say:

```csharp
  Dictionary<string, int> monthsByName = new Dictionary<string, int>();
```

or you can say, well, the compiler can clearly see that the expression I'm naming is a dictionary of whatever,
so I'll just write:

```csharp
  var monthsByName = new Dictionary<string, int>();
```

Critically, this isn't the same thing as dynamic typing, like you have in Javascript, Python, Ruby, or PHP.
The compiler still knows exactly what the type of `monthsByName` is, and it'll rightly yell at you if you try
to, say, call some bogus method name on it. This is a good thing!

F# takes the same concept to another level. Not only do you not have to declare the type of local variables,
you can also let the compiler figure out the return type and parameter types of your functions.

* Cue: show F# code example doing simple type inference

It can even tell when you have written a generic function, for example, it knows that this function will work with any
`IEnumerable<T>`.

* Cue: show F# code example doing generic type inferences

Again, this isn't dynamic typing. The compiler must know the type of every variable and will emit
a pitiful screech if it can't figure it out on its own.

* Cue: show F# code example with ambiguous types.

It'll also complain if it can figure out the types, but they don't line up:

* Cue: show F# code example with broken code.

So you get all the safety of static typing, with the compiler yelling at you if you make a mistake, but you can
often write code like you were in a dynamic language.

Now, some people favor being explicit about types to make code easier to understand, and I can understand that.
You are totally free to annotate the types explicitly whereever you want.
When you work with generics, there's even a middle ground,
where you can tell the compiler you're dealing with a `Dictionary<_, _>` but have it fill in the blanks.

* Cue: show F# code example with generic wildcard.

My advice is to be explicit about the types in your public API, but take advantage of the inference for private
methods and local variables.

* Cue: show F# code example with private inferred function, public annotated function.

This makes it very quick to take some code and wrap it in a little private utility function without having to
write the whole type signature, but still makes the interface to your code obvious.

### Let-bound functions, currying, and operators

So far the samples I've shown have had classes with methods, pretty much like we write in C#.
F# programmers are pretty big fans of static functions, so there is a shorthand for when you want to write
a bunch of static functions that you'll use frequently. This is called a module -- VB.NET has a similar feature
with the same name.

* Cue: show F# module with a constant (pi) and function (gcd?)

Now, a couple things you have to know about these functions. You declare them with the "let" keyword, same as
variables. In fact, anywhere that you could define a variable, you can define a function the same way.

The other thing that you'll notice is that you don't have to wrap the arguments to a function in parentheses.
Remember, F# is a "functional" programming language, so functions are such a big deal that applying a function
to an argument doesn't even take extra syntax. `f x` is implicitly `f(x)`.

* Cue: show examples with well-known functions like sqrt, factorial.

So what about `gcd x y`? Does that mean `gcd(x, y)`? That would be kind of weird, so the functional programmers came
up with another solution. All functions take one argument and output one result. If you have a function like
`gcd` that takes two arguments, that actually is a function that takes one argument and returns the function taking
the next argument. So `gcd x y` is really more like `(gcd(x))(y)`.

The reason I bother to go down this rabbit hole is to explain an operator that shows up all the time in F# code.
This operator is called the "pipe" operator and exists because code like this reads in an unnatural order, with
the last operation being the first one we see:

```
let binary = link (assemble (compile (preProcess (sourceCode)))
```

With the pipe operator, you write the same thing like this, which reads a little better:

```
let binary = sourceCode |> preProcess |> compile |> assemble |> link
```

The pipe operator is provided with F#. It's defined something like this.

```
let (|>) x f = f x
```

You can define your own operators in F#, but you obviously shouldn't do it too much since when people see
a new operator like `%%@*!`, that tends to be what they say.

* Cue: show comic panel

Anyway, the place you see this done the most is with the "Seq" module, which basically implements the same
functions you have with `System.Linq` on `IEnumerable<T>`. So for example, in C# you might write:

```csharp
users.Select(u => u.Name)
```

And in F# you write the same thing with:

```fsharp
users |> Seq.map (fun u -> u.Name)
```

You can say, "why does F# have to be a special snowflake and do the same damn thing in a different way?".
I agree with the sentiment, but to fair, F# was doing it in 2005 and C# got LINQ-to-objects in 2007, so give them
a break.

# Part 2: It's the best thing since sliced bread

Ok, for those of you who have survived the onslaught of pointless differences, here's the good part.
Please take this opportunity to yawn and stretch.

## Discriminated unions and pattern matching

The first feature I'm going to talk about seems a little bland at first, but I swear, if this was the only advantage
F# had it would still be worth using. This changed my life. It's called "discriminated unions".

So, here we are in North Carolina, and when you hear "discriminated unions", the first thing that comes to mind is
probably gay marriage. This isn't that, but it's still important to keep an open mind.

Let's say we're writing a calculator or spreadsheet app, and users can type in expressions like `(1 * 2) + 3`.
We'll have to have a type to represent an expression, and some logic to evaluate expressions.

Let's say we only support multiplication and addition, and here's how I'd represent this with a class hierarchy
in C# and F# -- same exact architecture, just different syntax.

* Cue: show code example

We've got a base class for expressions, and an abstract method to evaluate them. So far, looking good.

So let's say our little calculator language grows into a full-fledged programming language. Now we've got
a bunch more things we do with expressions other than evaluating them. Things like:

* Pretty-printing (or possibly displaying on a custom editor UI)
* Typechecking
* Optimizing expressions (e.g. x*x*x*x -> let square = x*x in square*square)
* Syntax highlighting
* Compiling to assembly or IL

We can add abstract methods for each of these, but each one is really a separate concern.
They may have their own domain-specific state you need to refer to (for example, to compile, you might need a
register allocator), but if you have each of these as a separate abstract method, you have to have every
class implementation see those guts. It's easy for the class library that implements each expression type
to end up like an octopus with the sum of all dependencies and internal member visibility.

* Cue: show octopus

It also becomes a nightmare to tell how a given domain works (say, syntax highlighting) because
you've got to look at the method override in each class, and the class files are liable to be huge since each
one has to deal with all these different domains.

F# discriminated unions are made to deal with this problem. They flip the problem around.

In OO, you specify the possible abstract methods to override in one place,
and can implement them with different subclasses in infinitely many places.

With DUs you specify the possible subclasses in one place, and can implement different methods
on those subclasses in infinitely many places.

```
type Expression =
    | Number of int
    | Add of Expression * Expression
    | Multiply of Expression * Expression
```

Then you could write an `evaluate` function like this -- anywhere, even in another project, using a feature
called pattern matching.

```
let rec evaluate (expr : Expression) =
    match expr with
    | Number n -> n
    | Add (left, right) -> evaluate left + evaluate right
    | Multiply (left, right) -> evaluate left * evaluate right
```

You can do the same for all your other situations, like syntax highlighting and optimization, and
now those concerns can be implemented in isolation, in separate projects with their own dependencies.

Important fact: the compiler will yell at you if you don't handle every case. This is nice when you add
a new case, say `| Divide of Expression * Expression` and the compiler points you to all the places in your
code you have to update to handle that new possibility.

### Why this vs. OO?

Am I trying to convince you this is better than OO with inheritance? Absolutely not! Each solution has its place.

For example, my favorite example of OO programming in .NET has to be `System.IO.Stream`.

You can use the `Stream` class to read/write data to a file, TCP connection, serial port, in-memory buffer, HTTP
request, and more. It would be awful if `Stream` were defined as a DU in F#, because it would be limited to the
cases the designer had thought of in the first place. As it is, you can extend `Stream` to handle whatever kind
of backing "stream" you can imagine.

What it boils down to is that if you're trying to represent different possibilities of *data*, you should use
a discriminated union. If you're trying to represent different implementations of a common *behavior* you should
use a class hierarchy. It is liberating to have both options at your disposal, instead of being limited to class
hierarchies.

Speaking of options, one of the places where DUs show up frequently in F# is in the `option` type.
This roughly corresponds to the `System.Nullable` type that is used in C# when you have a value type that could
be null, like `int?`. In C# you might write:

```
public static void Show(int? x) {
    if (x.HasValue)
    {
        Console.WriteLine(x.Value);
    }
    else
    {
        Console.WriteLine("(null)");
    }
}
```

In F# you can pattern match to do the same thing:

```
let show (x : option<int>) =
    match x with
    | Some value -> Console.WriteLine(value)
    | None -> Console.WriteLine("(null)")
```

The nice thing here is that you can't possibly refer to the value in the "None" branch. In C# you could easily
write something using `x.Value` in the `else` branch.

## 