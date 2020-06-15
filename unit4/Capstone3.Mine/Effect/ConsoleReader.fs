namespace Capstone3.Effect

type IConsoleReader =
    abstract member ReadLine: unit -> string
    abstract member ReadKey: unit -> char

module ConsoleReader =
    let readLine (cw: #IConsoleReader) = cw.ReadLine()
    let readKey (cw: #IConsoleReader) = cw.ReadKey()
