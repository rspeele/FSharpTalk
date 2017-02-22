# What is F#

* .NET language, but much more differences between C# and F# than between C# and VB.NET
* Do emphasise the ease of adding F# code to a largely C# solution
* Have been vaguely familiar with it for 4 years, finally jumped into using it on DC-UPS in March 2015

# Boring stuff

## FizzBuzz

* Whitespace sensitive
* Unit means void -- more or less
* If/else works as an expression, you don't need separate ternary

## Fibonacci

* Modules are just static classes with a lightweight way of writing them
* Let-bound functions, typically camelCase
* Functions can define inner functions
* Functions take arguments without parentheses, commas
* Full type inference -- compiler knows args are ints because they are compared with/added to integer literals
* You can still add type annotations whereever you feel it makes sense

## Rectangle

* More type inference
* Classes are pretty much written like C#
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

# OtherPatternMatching






