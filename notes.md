Bookmark: Page 45

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
