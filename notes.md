Bookmark: Page 186

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
* Talk about figure 15.6
