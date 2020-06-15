namespace Capstone3.Effect

type IConsoleWriter =
    abstract member WriteLine: string -> unit
    abstract member Write: string -> unit

module ConsoleWriter =
    let writeLine (cw: #IConsoleWriter) = cw.WriteLine
    let write (cw: #IConsoleWriter) = cw.Write
