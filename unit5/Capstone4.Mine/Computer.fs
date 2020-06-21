module Capstone4.Computer

open Capstone4

type IRetrieve = abstract member Retrieve: Customer -> RatedAccount option
let retrieve (computer: #IRetrieve) = computer.Retrieve

type ISave = abstract member Save: RatedAccount -> unit
let save (computer: #ISave) = computer.Save

type IDeposit = abstract member Deposit: Amount -> RatedAccount -> RatedAccount
let deposit (computer: #IDeposit) = computer.Deposit

type IWithdraw = abstract member Withdraw: Amount -> CreditAccount -> RatedAccount
let withdraw (computer: #IWithdraw) = computer.Withdraw
