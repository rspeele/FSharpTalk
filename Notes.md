# What is F#

* .NET language, but much more differences between C# and F# than between C# and VB.NET
* Do emphasise the ease of adding F# code to a largely C# solution
* Have been vaguely familiar with it for 4 years, finally jumped into using it on DC-UPS in March 2015
* F# interactive is handy -- show as calculator, hello world

# Boring stuff

## FizzBuzz

* Whitespace sensitive
* Unit means void -- more or less
* If/else works as an expression, you don't need separate ternary

## Fibonacci

* Modules are just static classes with a lightweight way of writing them
* Let-bound functions, typically camelCase
* Implicit return
* Functions can define inner functions
* Functions take arguments without parentheses, commas
* Full type inference -- compiler knows args are ints because they are compared with/added to integer literals
* You can still add type annotations whereever you feel it makes sense

## Rectangle

* More type inference
* More implicit returns
* Classes are not super different from C#
* Primary constructor: constructor arguments are implicitly private readonly fields
* Triple-slash comment is XML <summary> block, easy to slap on some quick notes for Intellisense.

# Fun stuff

## ExpressionClasses

* Writing a spreadsheet app with the start of a programming language
* We want to represent the different types of expressions in our language with an Expression type
* At first OO looks good -- subclasses for literals, operators, function calls, etc.
* Evalute method gets overridden on each subclass and all is well
* Can do this in both languages about equally well

* What if our language becomes a full-blown PL?
* Can have an abstract method for every concern, but end up with
  * Giant classes
  * Octopus of dependencies
  * Hard to follow how each logical component works

## ExpressionDU

* F# lets us flip the problem around
* Instead of writing all the possible methods in one place, and writing infinitely many data types implementing them...
* ...we can write all the data types in one place, and write infinitely many methods handling them.
* If you use this approach, you have to buy into the data described in your DU being plain-old data, immutable.

* Is this better? Of course not always.
* Great examples of OO:
  * Stream
  * TextWriter
  * ICollection<T>, IEnumerable<T>
  * WinForms Control
* Great examples of DU:
  * Any kind of syntax tree
  * Option
  * Result
  * Anywhere you would consider an enum (not flags) in C#
  * Data in transport
  * X,Y,Z,Other of string

  ## OtherDUs

  * Option vs System.Nullable

# OtherPatternMatching

* Pattern matching is a general purpose feature
* More things you can do with it than I have time for
* By the way tuples are a thing

# InlineFunctions

* There are some sort-of generic functions you can't write generically
* Math.Abs is a good example -- it has 7 overloads to support different numeric types
* F# lets you define functions like this "inline", supporting a much wider set of generic constraints
* Use one function to interact with many types that follow the same convention when an interface won't suffice
* TryParse example works for int.TryParse, DateTime.TryParse, decimal.TryParse, TimeSpan.TryParse etc etc.

# TypeProviderCSV

* Type providers are essentially compiler plugins
* Fill the same roles as code-generation tools, but:
  * Much less effort to use
  * No extra build step to manage
  * No generated code artifacts in source control
* Type x = ... generic parameter is a string, this runs when you build your program or in VS for Intellisense

# TypeProviderHTML

* Scraping HTML is a painful task and the code is brittle -- subject to break if the page changes
* Can't fix the second part, but can make it very easy to scrape
* Not crazy about having the compiler make a network call to build my program, in reality unless it was
  just a quick script would use a local copy of the file

# ComputationExpressions

* Could spend an hour talking about this feature alone
* Start with the basics: IEnumerable<T> and yield
* In F# it is an expression (no surprise)
* Also used for async which is like C# async/await


