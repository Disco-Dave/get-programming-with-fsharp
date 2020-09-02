let x a b = a + b

x 2 3


type OptionBuilder() =
    member _.Bind(opt, func) = Option.bind func opt
    member _.Return value = Some value

let option = OptionBuilder()


option {
    let! x = Some 1
    let! y = None
    return x + y }

type ResultBuilder() =
    member _.Bind(opt, func) = Result.bind func opt
    member _.Return value = Ok value

let result = ResultBuilder()

result {
    let! x = Ok 1
    let! y = Ok 10
    return x + y }

seq {
    for i in [ 1 .. 10 ] do
        if i * i > 10 then i + 2
}
|> Seq.toList

type State<'state, 'a> = 'state -> ('state * 'a)

module State =
    let bind f value =
        fun state ->
            let (newState, rawValue) = value state
            (f rawValue) newState

    let wrap value = fun state -> state, value

    let gets(f: 's -> 'a): State<'s, 'a> = fun state -> (state, f state)

    let get: State<'s, 's> = fun state -> (state, state)

    let put(state: 's): State<'s, Unit> = fun _ -> (state, ())

type StateBuilder() =
    member _.Bind(st, func) = State.bind func st
    member _.Return value = State.wrap value

let state = StateBuilder()

let modify(f: 'state -> 'state): State<'state, 'state> =
    state {
        let! oldState = State.get
        let newState = f oldState
        do! State.put newState
        return newState
    }
