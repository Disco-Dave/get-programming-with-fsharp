namespace Capstone4

type PositiveAmount = private PositiveAmount of decimal

module PositiveAmount =
    let unwrap (PositiveAmount amount) = amount

    let make money = 
        if money >= 0M then
            Some <| PositiveAmount money
        else
            None
