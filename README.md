# What is F#?

F# is yet another .NET language, like C# or VB.NET.
It was born in Microsoft Research, a Microsoft rehabilitation facility for academics.

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
about a couple little things just so you know the score when you read a snippet of F# code.

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

Again, this isn't dynamic typing. The compiler must be able to deduce the type of every variable and will emit
a pitiful screech if it can't:

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

This makes it very low-friction to take some code and wrap it in a little private
utility function without having to write the whole type signature, but still makes the interface to your
code obvious.

### Let-bound functions

F# has something called a module. This concept is also found in VB.NET and you can do something similar in C# 6.
This is just a static class that you can import with an "open" directive, and use the functions defined in it
without qualifying them with the class name.

