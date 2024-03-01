# MathEvaluator
A C# library to evaluate mathematical expressions.

The expressions can contain spaces or not and the user is helped to write the fastest expression. eg: 2*-2 instead of 2 * (-2) or .25 instead of 0.25 or 2(5+3) instead of 2 * (5+2)

Both **,** and **.** can be used as the decimal caracter.

For successive same operations without parenthesis, the result is evaluated from the left to the rigth. eg: 10/4/2 = (10/4)/2 = 1.25 or 2^2^3 = (2^2)^3 = 64

### Division by 0

We allow the user to decide what to do in the case of division by `0` thanks to the static property `MathEvaluator.RaiseDivideByZeroException`, by default set to `true`.
In the case of this property is equal to `true` and that a division by 0 is applying, it will raise a `DivideByZeroException`. Else it will return either `+∞` or `-∞` depending on the numerator value. If the `numerator` is equal to 0, there is a `invalid domain` that is explained in the followed paragraph.

### Invalid domain

We allow the user to decide what to do in the case of an `invalid domain` operation (eg: 0/0, sqrt(x) where x is negative, ln(x) where x <= 0) thanks to the static property `MathEvaluator.RaiseDomainException`, by default set to `true`.
In the case of this property is equal to `true` and that an `invalid domain` operation is appliyng, it will raise a `DomainException`. Else it will return `NaN`.

## Basic operators (+, -, *, /)

| Expression | Result | Condition before applying the evaluation / Remarks |
| ---------- | ------ | -------------------------------------------------- |
| 2 + 10 | 12  |
| 2 + 010 | 12  |
| 2 + 0 + 4+1 | 7 |
| -8+12.5 | 4.5 |
| -8,3+12.5 | 4.2 | Remarks: Exact value was 4.1999999999999993 |
| -8,4+12.5 | 4.1 |
| -(5) | -5 |
| -(5+3) | -8 |
| -(-5+3) | 2 |
| -(-5-3) | 8 |
| -(-(-5)) | -5 |
| 8-3 | 5 |
| 5*2 | 10 |
| 5*2*3.5 | 35 |
| 5*-3 | -15 |
| -4*3 | -12 |
| -4*-5 | 20 |
| (-4)*(-3) | 12 |
| 6/2 | 3 |
| (5/2)/.25 | 10 |
| 9.3/-3 | -3.1 |
| -4/3 | -4/3  |
| -4/-5 | 0.8 |
| 5/(2/.25) | 0.625 |
| -5/(2/.25) | -0.625 |
| -5/(2/-.25) | 0.625 |
| -5/-(-2/-.25) | 0.625  |
| 8/2/0.25 | 16 |
| 3/0 | +∞ | `RaiseDivideByZeroException` = `false` |
| 3/-0 | +∞ | `RaiseDivideByZeroException` = `false`  Remarks: It's well +∞ and no -∞.|
| -3/0 | -∞ | `RaiseDivideByZeroException` = `false` |
| 2/0 | DivideByZeroException |
| 0/0 | DomainException | `RaiseDivideByZeroException` = `false` |
| 0/0 | NaN | `RaiseDivideByZeroException` = `false` and `RaiseDomainException` = `false` |
| 1/(1/0) | 0 | `RaiseDivideByZeroException` = `false` |
| -1/(1/0) | 0 | `RaiseDivideByZeroException` = `false` |
| -8*2+10-5 | -11 |
| (5 + 2)(2(3 / 4) + 3 *.5) | 21 |
| -(5 + 2)(2(3 / 4) + 3 *.5) | -21 |
| -(5 + 1)(-2(-3 / 4) + 3 *.5) | -18 |
| -(5 + 1)(-2.-(3 / 4) + 3 *.5) | 7.5 |
| -(5 + 1)(-2-(-3 / 4) + 3 *.5) | -1.5 |

## Power (^)

| Expression | Result | Condition before applying the evaluation / Remarks |
| ---------- | ------ | -------------------------------------------------- |
| 2^3 | 8 |
| 2^0 | 1 |
| (1+3)^(1+1) | 16 |
| 2(1+3)^(1+1) | 32 |
| (2(1+3))^(1+1) | 64 |
| (-2)^3 | -8 |
| (-2)^4 | 16 |
| -2^4 | -16 |
| (1-1)^3 | 0 |
| (2^2)^3 | 64 |
| 2^2^3 | 64 |
| 2^(2^3) | 256 |
| 0^0 | DomainException |
| (2+1-3)^(2-2) | DomainException |
| 0^0 | NaN | `RaiseDomainException` = `false`
| 2^(4.3/0) | +∞ | `RaiseDivideByZeroException` = `false` |
| 0^(1/0) | 0 | `RaiseDivideByZeroException` = `false` |
| -0^(1/0) | 0 | `RaiseDivideByZeroException` = `false` |
| (1/0)^(1/0) | +∞ | `RaiseDivideByZeroException` = `false` |
| (-1/0)^(1/0) | -∞ | `RaiseDivideByZeroException` = `false` |

## Factorial (!)

| Expression | Result | Condition before applying the evaluation / Remarks |
| ---------- | ------ | -------------------------------------------------- |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
|  |  |
