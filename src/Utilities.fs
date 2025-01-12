module Utilities

let add x y = x + y
let multiply x y = x * y

let rec factorial n =
    if n <= 1 then 1
    else n * factorial (n - 1)

let applyFunctionTwice f x = f (f x)

let square x = x * x