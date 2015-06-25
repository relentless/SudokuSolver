module SudokuSharp.Solver.Tests

open System
open SudokuSharp.Solver
open FsUnit
open NUnit.Framework

[<Test>]
let test1()=
    let ans = Solver().solve (Array2D.create 9 9 (Nullable<int>()))
    ans.GetLength(0) |> should equal 9

[<Test>]
let ``solve single row with missing 9``()=
    let row = [for i in 1..8 do yield Some(i)]@[None] |> List.toArray
    let expected = [|[for i in 1..9 do yield Some(i)] |> List.toArray|]

    row |> Solver().solveRow |> should equal expected 

[<Test>]
let ``solve single row with missing 1``()=
    let row = None::[for i in 2..9 do yield Some(i)] |> List.toArray
    let expected = [|[for i in 1..9 do yield Some(i)] |> List.toArray|]

    row |> Solver().solveRow |> should equal expected 

[<Test>]
let ``solve single row with two missing numbers gives both possible solutions``()=
    let row = [for i in 1..7 do yield Some(i)]@[None;None] |> List.toArray
    let expected = [|
        [for i in 1..9 do yield Some(i)] |> List.toArray
        [for i in 1..7 do yield Some(i)]@[Some(9);Some(8)] |> List.toArray|]

    row |> Solver().solveRow |> should equal expected 

[<Test>]
let ``try factorial``()=
    let row = [for i in 1..9 do yield None] |> List.toArray
    
    row |> Solver().solveRow |> Array.length|> printfn "%A"