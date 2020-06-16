module Capstone3.Computer

open Capstone3

type IRetrieveAccount = abstract member Retrieve: Customer -> Account
let retrieveAccount (computer: #IRetrieveAccount) = computer.Retrieve

type IAlterAccount = abstract member Alter: Account -> Request -> Account
let alterAccount (computer: #IAlterAccount) = computer.Alter   
