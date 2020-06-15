namespace Capstone3.Effect

type IConsoleReader =
    abstract member ReadLine: unit -> string
    abstract member ReadKey: unit -> char

module ConsoleReader =
    let writeLine (cw: #IConsoleReader) = cw.ReadLine
    let write (cw: #IConsoleReader) = cw.ReadKey
