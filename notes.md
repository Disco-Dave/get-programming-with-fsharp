Bookmark: Page 92

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

### Lesson 8: Capstone 1
