module Capstone3.FileSystem

type ICreateDirectory = abstract member CreateDirectory: string -> unit
let createDirectory (fs: #ICreateDirectory) = fs.CreateDirectory

type IDoesFileExist = abstract member DoesFileExist: string -> bool
let doesFileExist (fs: #IDoesFileExist) = fs.DoesFileExist

type IReadLines = abstract member ReadLines: string -> string seq
let readLines (fs: #IReadLines) = fs.ReadLines

type ICreateFile = abstract member CreateFile: string -> unit
let createFile (fs: #ICreateFile) = fs.CreateFile

type IAppendText = abstract member AppendText: string -> string -> unit
let appendText (fs: #IAppendText) = fs.AppendText
