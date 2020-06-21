namespace Capstone4

type Amount = private Amount of decimal

module Amount =
    let unwrap (Amount amount) = amount

    let make money = 
        if money >= 0M then
            Some <| Amount money
        else
            None
