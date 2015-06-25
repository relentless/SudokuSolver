namespace SudokuSharp.Solver

open System

type Solver() = 
    let addNumberToFirstMissing number row =
        let rec addNumberRecursively number = function
            | [] -> []
            | None::tail -> Some(number)::tail
            | head::tail -> head::(addNumberRecursively number tail)
        addNumberRecursively number (row |> Array.toList) |> List.toArray
    
    member this.solve (problem:Nullable<int>[,]) =
        Array2D.create 9 9 <| Nullable<int>()

    member this.solveRow (row:int option[]) =

        let tryMissingNumbers row missingNumbers = 
            missingNumbers 
            |> Array.map (fun missingNum -> 
                this.solveRow( row |> addNumberToFirstMissing missingNum ))

        let missingNumbers =
            [|1..9|]
            |> Array.filter (fun x -> 
                not (row |> 
                    Array.exists (function
                        | Some(value) -> value = x
                        | _ -> false)))

        match Seq.length missingNumbers with
        | 1 -> 
            [|row |> addNumberToFirstMissing missingNumbers.[0]|]
        | _ -> 
            missingNumbers 
            |> tryMissingNumbers row 
            |> Array.concat
                                            

         