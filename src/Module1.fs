module Module1

open Utilities

let run() =
    let sum = add 3 5
    let product = multiply 4 6
    let fact = factorial 5
    let squared = applyFunctionTwice square 3

    printfn "Module1: The sum of 3 and 5 is %d" sum
    printfn "Module1: The product of 4 and 6 is %d" product
    printfn "Module1: The factorial of 5 is %d" fact
    printfn "Module1: The result of applying square function twice to 3 is %d" squared