module Unit4

type SoccerResult =
    { HomeTeam: string
      AwayTeam: string
      HomeGoals: int
      AwayGoals: int }

let results =
    [ { HomeTeam = "Missiville"
        AwayTeam = "Ronaldo City"
        HomeGoals = 1
        AwayGoals = 2 }
      { HomeTeam = "Missiville"
        AwayTeam = "Bale Town"
        HomeGoals = 1
        AwayGoals = 3 }
      { HomeTeam = "Bale Town"
        AwayTeam = "Ronaldo City"
        HomeGoals = 3
        AwayGoals = 2 }
      { HomeTeam = "Bale Town"
        AwayTeam = "Messiville"
        HomeGoals = 2
        AwayGoals = 1 } 
      { HomeTeam = "Ronaldo City"
        AwayTeam = "Messiville"
        HomeGoals = 4
        AwayGoals = 2 } 
      { HomeTeam = "Ronaldo City"
        AwayTeam = "Bale Town"
        HomeGoals = 1
        AwayGoals = 2 } 
    ]

// Write a function to pick the team who won the most away games in the season
let mostAwayGamesWon soccerResults =
    let awayWins =
        soccerResults
        |> Seq.filter(fun { HomeGoals = hg; AwayGoals = ag } -> ag > hg)
        |> Seq.countBy(fun { AwayTeam = at } -> at)
    if Seq.isEmpty awayWins then
        None
    else
        awayWins
        |> Seq.maxBy snd
        |> fst
        |> Some

mostAwayGamesWon results
