namespace AsyncModuleExecutor

module Algorithm =

    open System

    // Function to calculate the distance between two points
    let distance (x1, y1) (x2, y2) =
        Math.Sqrt((x2 - x1) ** 2.0 + (y2 - y1) ** 2.0)

    // Function to calculate the total distance of a given path
    let totalDistance path =
        let rec loop acc = function
            | [] | [_] -> acc
            | p1 :: p2 :: rest -> loop (acc + distance p1 p2) (p2 :: rest)
        loop 0.0 path

    // Function to generate all permutations of a list
    let rec permutations = function
        | [] -> [[]]
        | x :: xs -> List.collect (fun perm -> [for i in 0..List.length perm -> List.insertAt i x perm]) (permutations xs)

    // Function to solve the Traveling Salesman Problem using brute-force
    let solveTSP points =
        let allPaths = permutations points
        allPaths
        |> List.map (fun path -> path, totalDistance path)
        |> List.minBy snd