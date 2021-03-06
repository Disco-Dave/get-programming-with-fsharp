Bookmark: Page 297

## Links
* [F# Language Reference](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/)
* [F# Software Foundation](https://fsharp.org/)
* [Fable (F# to JavaScript)](https://fable.io/)
* [Ionide](https://ionide.io/)


## Welcome
* F# is mature and has lineage to [ML](https://en.wikipedia.org/wiki/ML_(programming_language))
* F# has embraced open source from its inception. C# and other Microsoft products have only
  recently started to emphasize open source.
* F# is considered to be *Functional First*, meaning that the language does encourage you to
  write your software in a functional paradigm but doesn't force you. You can write imperative
  and object-oriented code as well. That last point is important since .NET is an object-oriented
  runtime so having the ability to do OO and imperative programming makes it easier for F# to interact
  with .NET ecosystem.
* The language thrives on building small pieces and composing them together, as opposed to large class
  hierarchies commonly found in object-oriented programming.
* The author defines a functional programming language as a language that has the following qualities:
    * Immutability 
    * Expressions
    * Functions as values
* The author recommends F# and functional programming for boring line-of-business applications.

## Unit 1: F# and Visual Studio

### Lesson 1: The Visual Studio Experience
* The author recommends using Visual Studio 2015 and Visual F# Power Tools extension
* We are probably fine to use Visual Studio 2019 and the .Net core version of F#
* He also mentions VS Code and Ionide which is the best option for anyone on Linux or Mac

### Lesson 2: Creating Your First F# Program
* Dotnet has many templates for F# out of the box to view them you can run the following: `dotnet new --help | grep F#`
* F# uses an attribute `[<EntryPoint>]` to mark the main function of the application.
* The signature of the main function must be `string [] -> int`
* To create a new console application run the following: `dotnet new console -o <Folder Name Here> -n <Project Name Here> -lang f#`
* To run the application just run `dotnet run` inside of the folder you just created.
* Debugging F# works in the same in Visual Studio as it does for C#. Same story applies to Visual Studio Code.
* **Note**: Notice how the string formatter of `printf`, `printfn`, and `sprintf` are type checked. Very cool!

### Lesson 3: The REPL - Changing How Develop
* The REPL or *Read Evaluate Print Loop* helps quickly test out small snippets of code.
* The F# REPL can be started by running the following: `dotnet fsi`
* It mentions the C# has a primitive REPL in VS2015 called C# Interactive, which appears to be
  absent in .Net Core 3.1.
* Note that you have to terminate statements in `fsi` with `;;`. The only real reason for this is that it's what ocaml did.
* To run a script run the following `dotnet fsi name_of_script.fsx`
* To load a script and play with it in the REPL run `dotnet fsi --use:name_of_script.fsx`

## Unit 2: Hello F#

### Lesson 4: Saying a Little, Doing a Lot
* Overview:
    * Basic syntax, in particular the `let` keyword
    * Write more complex functions and values
    * Scoping
* Author reiterates the following:
    * The syntax is lightweight and the type system is powerful.
    * The language is designed to solve *complex* problems with *simple* code.
    * The goal is to write code without thinking first about design patterns.
* Semicolons are not required unless putting multiple statements on one line.
* `let` keyword
    * [let bindings in F# language reference](https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/functions/let-bindings)
    * Used to bind any value to a symbol
    * May or may not include a type annotation
    * May be used to define functions
* "It's better to think of let bindings as copy-and-paste directives; wherever you see the symbol,
   replace it with the value that was originally assigned during the declaration."
   * This is actually the concept of [referential transparency](https://en.wikipedia.org/wiki/Referential_transparency)
   * The same should apply to functions as well
* The language does allow [shadowing](https://en.wikipedia.org/wiki/Variable_shadowing)
    * Important for use with the REPL
    * Not the most useful outside of the REPL
* Whitespace is used for scoping.
* Brackets are not used, except for computation expressions.
* There is no `return` keyword, except for computation expressions. The last line of an expression
  is the return value.
* `public` is the default accessibility modifier.
* Functions defined with `let` are static
* We can nest functions or values an arbitrary amount
* F# is strict about the ordering of functions and files.
* **ACTION:** install a Windows 10 virtual machine to try out the win forms example

### Lesson 5: Trusting the Compiler
* Overview:
    * Type inference
* Unlike C#, F# can infer types of parameters and return types.
* F# does not allow implicit conversions
* Limits of type inference
    * Can have trouble inferring a type if all you do is call method for example:
        * `let getLength name = name.Length` won't compile
        * `let getLength (name: string) = name.Length` will compile
    * Overloaded methods on classes
* F# can also infer generic types by specifying an underscore or omitting them all together.
* The type inference will always try to infer the most generic form.

### Lesson 6: Working with Immutable Data
* Overview:
    * Basic syntax for working with immutable and mutable data
    * Reasons to consider immutability as the default
    * Simple examples of working with immutable values to manage changing state
* Using immutable data structures isn't inherently slower
* Easier to reason to about and test
* Limits the need to hide data since it's immutable
* Limits the need to have locks in multi threaded environments 

### Lesson 7: Expressions and Statements
* Overview:
    * Differences between expressions and statements
    * Pros and cons of both
    * How expressions when coupled with the type system allow you to write more succinct code
* Statements do not return anything, and always have side effects
* Expressions always do return something, and may have side effects (hopefully not)
* C# is strongly statement based, the majority of its program flow is about side effects and statements.
* F# is heavily expression based
* No such thing as a void function in F#. Must use `unit` instead.
* All program flow branching mechanisms are expressions
* All values are expressions
* When everything are expressions that there is no need for a return keyword.
* If you want to disregard a function's result that isn't unit, you have to use the ignore function.

## Unit 3: Types and Functions

### Lesson 9: Shaping Data with Tuples
* Overview:
    * How tuples are used within F#
    * When to use them and when not
    * How tuples work with type inference
    * How tuples relate to the rest of .NET
* A function that parses a string and returns the forename and surname is a great
  use case for using a tuple.
* Apparently F# had better support for tuples before F# did
* You should use tuples for internal helpers and for storing intermediary state
* Tuples types show up something like `string * int * double`
* You can deconstruct tuples
* You can nest tuples
* When deconstructing tuples you can discard elements with an underscore
* Type inference works how you would expect with tuples in F#
* F# handles out params from C# and VB really well
* Tuples are often not the best choice for public APIs

### Lesson 10: Shaping Data with Records
* Overview:
    * What are Records
    * Records compared to C# classes
    * How to alter records while still retaining immutability
    * Tips for working with records
* Benefits over POCO
    * Immutable
    * Automatic structural equality of the entire object graph
    * Enforces that all fields are set
* The compiler infers what record you mean by the properties you're initializes
* You can be explicit about the type by either putting the type on the left side
  of a let or prefixing field with the type name. As shown on page 118
* The compiler can also infer the record you mean by how use an instance of a record
* We can change values of a record with a *copy and update* like syntax using the 
  `with` keyword.
* **NOTE:** "This way of working is a great fit for event-based architectures, where you record
  all changes to data over time as immutable events and versions of records."
* We can use shadowing to avoid having to come up with nonsense names.

### Lesson 11: Building Composable Functions
* Overview:
    * F# functions compared to methods
    * Partial Application
    * `|>` and `>>`
* **NOTE:** No overloading with `let` bound functions
* Difference between tupled and curried functions
* Forward pipe operator `let (|>) (x: 'a) (f: 'a -> 'b): 'b = f x`
* Compose operator `let (>>) (g: 'a -> 'b) (f: 'b -> 'c): ('a -> 'c) = fun a -> f (g a)`
* The forward pipe operator should feel familiar to anyone who does shell scripting. `|`
* "You’ll find that the pipeline is extremely useful for composing code together into a
   human-readable domain-specific language (DSL)." This what the author does in *Domain
   Modelling Made Functional*
* You can use partial application to build simple wrapper like functions, like the date time example
  on page 128

### Lesson 12: Organizing Code without Classes
* Overview:
    * Namespaces
    * Modules
    * How to use them with a standalone application
* In F# we can follow a few simple rules to organize code:
    * Place related types together in namespaces
    * Place related stateless functions together in modules
* Namespacing works exactly the same as it does in C# and VB.Net
* We can open namespaces using the `open` keyword. Fun fact: this can be anywhere in your code
* You can only store types and modules in namespaces
* Modules can hold let-bound stuff
* Modules are sort of like static classes
* Modules are sort of like namespacing but can also store functions
* You may also arbitrary nest namespaces and modules
* Namespaces may span mutliple files, where modules can't
* Use modules to store functions and types that are tightly coupled to those functions
* Use namespaces to logically group types and modules

### Lesson 13: Achieving Code Reuse in F#
* High-order functions
* Pass functions or interfaces as the first argument of functions. Useful for partial application

## Unit 4: Collections in F#

### Lesson 15: Working with Collections in F#
* Overview:
    * Introduce F# Collection types
    * How to think about transformations in terms of pipelines
    * How to use immutable collections
* There are three modules to be familiar with:
    1. Seq
    2. List
    3. Array
* Declarative vs imperative
* Show indexing and slicing
* F# List are native. They are linked lists and immutable. Unlike C#'s which are ArrayLists and mutable
* Talk about figure 15.6 (::) and (@)

### Lesson 16: Useful collection functions
* Overview:
    * The most common collection functions
    * Comparison with System.Linq
    * Difference between imperative and declarative
    * Moving between collection types
* `map` - converts items inside of a collection from one shape to another shape. Similar to `.Select`
* See also: `mapi`, `map2`, `map3`, `mapi2`, `indexed`
* `iter` - same as map, but the mapping function returns `unit`. Useful for side-effects.
* See also: `iteri`, `iter2`, `iter3`, `iteri2`
* `collect` - Similar to `.SelectMany`. More commonly know as `FlatMap` or `Bind`
* The author notes that you can implement `map` in terms of `collect`. That's because `Bind` is part of a monad, and all monad's are functors.
* `pairwise` - takes a list and returns a new list of tuple pairs of the original adjacent items. Similar to `windowed`
* `groupBy` - simpler version of `.GroupBy`
* `countBy` - similar to `groupBy` but returns a count instead of a collection
* `partition` - divides a collection into two based on a predicate
* `chunkBySize` - is the same function we use called `.Batch`
* Aggregates: `sum`, `average`, `max`, `min`, `fold`
* All of three of the modules have functions that allow you to easily convert between the two.

### Lesson 17: Maps, Dictionaries, and Sets
* Overview:
    * Working with the standard generic dictionary in F#
    * Creating immutable dictionaries with `IDictionary`
    * Using the F# specific `Map`
    * Using the F# specific `Set`
* Remember when using Generic Dictionary F# index's using a dot
* We can omit generic types with `_` or just omit them completely
* The dictionary from System.Collections.Generic is mutable :(. But Map from F# is not
* We use the `Map` module to construct and manipulate Maps
* We can construct and manipulate immutable sets with `Set` module


### Lesson 18: Folding your way to success
* Overview:
    * Understanding aggregations and accumulation
    * Avoiding mutation through fold
    * Building rules engines and functional chains
* `fold` - an alternative to recursion
* seq computation expression


## Unit 5: The pit of success with the F# type system

### Lesson 20: Program flow in F#
* Overview:
    * for/while loops
    * if/then expressions
    * switch/case statements
    * pattern matching
* Comprehensions
* Pattern matching
    * Exhaustive checking
    * Warns us about unreachable patterns
    * Test against single source
    * Match using patterns that represent specific cases
    * Expression
    * Wild card is _
    * Guards
    * Top down
* Pattern match against lists, arrays, records
* Should prefer pattern matching

### Lesson 21: Modeling Relationships in F#
* Overview: Discriminated Unions
* Ways to think of them:
    1. Similar to type hierarchy, but it is *closed*. Must define all sub types up front, can't add more in other places.
    2. A form of C#-style enums, but with the ability to add metadata to each enum case.
* Forces us to exhaustively pattern match to use them.

### Lesson 22: Fixing the Billion-Dollar Mistake
* Overview: Optional data
* `null` is bad. Tony Hoare regrets inventing and even C# compiler team has admitted they wish they never added it.
* F# makes it difficult to assign `null` to any F# declared type. It only exists to deal with C#/VB.Net
* To handle optional values we should use the `Option<'a>` type instead.

### Lesson 23: Business rules as code
* Make illegal states *unrepresentable*
* Single-case discriminated unions can be used to create wrapper types.
    * Documentation to our types
    * Prevent accidentally mixing up values
    * Can make the constructor private to establish as "smart-constructor" 
* Clever use of discriminated unions can be used to encode business rules
* We can use mark types to indicate the state a value is in, like is `GenuineCustomer`
* Use the `Result<'a>` type to represent things that may fail.

## Unit 6: Living on .NET

### Lesson 25: Consuming C# from F#
* It's easy to create hybrid solutions
* Visual studio 2019 works as you'd expect
* Can easily consume other assemblies, files, and scripts within an F# script
* Constructors of objects are just functions
* How to implement interfaces
    * Requires up-cast
    * Explicit implementation
* Object expressions
* Converting back and fourth between null/Nullable with Option functions
    * Option.ofObj/Option.toObj
    * Option.fromNullable/Option.toNullable

### Lesson 26: Working with nuget packages
* You can reference nuget packages just fine
* You don't *need* paket
* The .net core cli has a nicer interface for nuget packages

### Lesson 27: Exposing F# types and functions to C#
* Records appear as classes with non default constructors and properties with only getters
* Namespaces are the same
* Modules are static classes
* Functions appear in their tupled form, except for functions that are partially applied.
* Discriminated Unions may be kinda clumsy. TODO: See what if it's any better with C# 8?
* Try to avoid exposing `List`
* CLIMutable
* Units of Measure and Type Providers are erased at compile-time
