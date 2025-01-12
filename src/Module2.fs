module Module2

open Utilities

let run() =
    let sum = add 10 20
    let product = multiply 7 8
    let fact = factorial 6
    let squared = applyFunctionTwice square 4

    printfn "Module2: The sum of 10 and 20 is %d" sum
    printfn "Module2: The product of 7 and 8 is %d" product
    printfn "Module2: The factorial of 6 is %d" fact
    printfn "Module2: The result of applying square function twice to 4 is %d" squared