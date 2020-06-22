module Capstone4.Communicate

type ISay = abstract Say: string -> unit
let say (communicate: #ISay) = communicate.Say

type ISayLine = abstract SayLine: string -> unit
let sayLine (communicate: #ISayLine) = communicate.SayLine

type IAskChar = abstract AskChar: unit -> char
let askChar (communicate: #IAskChar) = communicate.AskChar()

type IAskLine = abstract AskLine: unit -> string
let askLine (communicate: #IAskLine) = communicate.AskLine()

