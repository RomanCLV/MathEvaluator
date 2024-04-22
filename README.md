# MathEvaluator
A C# library to evaluate mathematical expressions.

Summary:

- General
	- Division by 0
	- Invalid domain
	- Angle unit
- Basic operators (+, -, *, /)
- Power (^)
- Factorial (!)
- Functions
- Constants
- Use variables in expressions
- Permanent variables and expressions
- Sum, Product
- Integral

## General

The expressions can contain spaces or not and the user is helped to write the fastest expression. eg: 2*-2 instead of 2 * (-2) or .25 instead of 0.25 or 2(5+3) instead of 2 * (5+2)

The **.** is used as the decimal caracter.

The **,** is used as the function parameter separator.

For successive same operations without parenthesis, the result is evaluated from the left to the rigth. eg: 10/4/2 = (10/4)/2 = 1.25 or 2^2^3 = (2^2)^3 = 64

However, the operation 2!^3! is interpreted as (2!)^(3!) and not (2!^3)!.

### Division by 0

We allow the user to decide what to do in the case of division by `0` thanks to the static property `MathEvaluator.Parameters.RaiseDivideByZeroException`, by default set to `true`.
In the case of this property is equal to `true` and that a division by 0 is applying, it will raise a `DivideByZeroException`. Else it will return either `+∞` or `-∞` depending on the numerator value. If the `numerator` is equal to 0, there is a `invalid domain` that is explained in the followed paragraph.

### Invalid domain

We allow the user to decide what to do in the case of an `invalid domain` operation (eg: 0/0, 0.25!, sqrt(x) where x is negative, ln(x) where x <= 0) thanks to the static property `MathEvaluator.Parameters.RaiseDomainException`, by default set to `true`.
In the case of this property is equal to `true` and that an `invalid domain` operation is applying, it will raise a `DomainException`. Else it will return `NaN`.

### Angle unit
We allow the user to set if all the angle are considered as degrees or radians thanks to the property `MathEvaluator.Parameters.AngleAreInDegrees`, by default set to `true`.

## Basic operators (+, -, *, /)

| Expression | Result | Condition before applying the evaluation / Remarks |
| ---------- | ------ | -------------------------------------------------- |
| 2 + 10 | 12  |
| 2 + 010 | 12  |
| 2 + 0 + 4+1 | 7 |
| -8+12.5 | 4.5 |
| -8.3+12.5 | 4.2 | Remarks: Exact value was 4.1999999999999993 |
| -8.4+12.5 | 4.1 |
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
| 3/-0 | -∞ | `RaiseDivideByZeroException` = `false` |
| -3/0 | -∞ | `RaiseDivideByZeroException` = `false` |
| -3/-0 | +∞ | `RaiseDivideByZeroException` = `false` |
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

We allow the user to decide what to do in the case of an `invalid domain` factorial operation (eg: 5.25!, (-0.5)!, (-1)!)) thanks to the static property `MathEvaluator.Parameters.UseGammaFunctionForNonNaturalIntegerFactorial`, by default set to `true`.
In the case of this property is equal to `true` and that a such operation is applying, it will use the Gamma (Γ) function (since x! = Γ(x+1)).


However, if the Gamma function is asked for a negative integer, it will raise a `DomainException` or return `NaN`.


See: [https://en.wikipedia.org/wiki/Gamma_function](https://en.wikipedia.org/wiki/Gamma_function) to know more anout this function.

The higher rank of factorial you can ask is `170`. If you ask higer, it will return `+∞`.

| Expression | Result | Condition before applying the evaluation / Remarks |
| ---------- | ------ | -------------------------------------------------- |
| 0! | 1 |
| 1! | 1 |
| 5!*6 | 720 |
| 5!(6) | 720 |
| 5!6 | 720 |
| 2!^(3!) | 64 |
| 2!^3! | 64 |
| (5!)/(5!) | 1 |
| 5!/5! | 1 |
| 170! | 7.25741561530799E+306 |
| 171! | +∞ |
| -0! | -1 |
| -1! | -1 |
| (-0.5)! | DomainException | `UseGamma...` = `false` |
| 0.25! | DomainException | `UseGamma...` = `false` |
| (-0.5)! | 1.77245385090205 |
| 0.25! | 0.906402477055538 |
| 0.25! | NaN | `UseGamma...` = `false` and `RaiseDomainException` = `false` |
| (-1)! | DomainException |
| (-1)! | NaN | `RaiseDomainException` = `false` |
| 3!! | 720 | 3!! -> (3!)! |

## Functions

You can find bellow all the implemented functions:

General functions:

| Shortname | Name & Description | Usage |
| --------- | ------------------ | ----- |
| abs(x) | Absolute | abs(expression) |
| exp(x) | Exponential | exp(expression) |
| ln(x) | Natural logarithm: The logarithm whose base is the Euler number e. | ln(expression) |
| log(x) | Logarithm of base 10 | log(expression) |
| log(x, base) | Logarithm of a specified base | log(expression, expressionBase) |
| sqrt(x) | Square Root | sqrt(expression) |
| sqrt(x, root) | Nth Root | sqrt(expression, expressionRoot) |
| gamma(x) | Gamma: Extension of the factorial function to complex numbers. | gamma(expression) |

Trigonometric functions:

| Shortname | Name & Description | Usage |
| --------- | ------------------ | ----- |
| cos(x) | Cosine | cos(expression) |
| sin(x) | Sine | sin(expression) |
| tan(x) | Tangent | tan(expression) |
| acos(x) | Arccosine | acos(expression) |
| asin(x) | Arcsine | asin(expression) |
| atan(x) | Arctangent | atan(expression) |
| csc(x) | Cosecant | csc(expression) |
| sec(x) | Secant | sec(expression) |
| cot(x) | Cotangent | cot(expression) |
| cosh(x) | Hyperbolic cosine | cosh(expression) |
| sinh(x) | Hyperbolic sine | sinh(expression) |
| tanh(x) | Hyperbolic tangent | tanh(expression) |
| csch(x) | Hyperbolic cosecant | csch(expression) |
| sech(x) | Hyperbolic secant | sech(expression) |
| coth(x) | Hyperbolic cotangent | coth(expression) |

Angle conversion functions:

| Shortname | Name & Description | Usage |
| --------- | ------------------ | ----- |
| rad(x) | Radian | rad(expression) |
| deg(x) | Degree | deg(expression) |

Decimal part functions:

| Shortname | Name & Description | Usage |
| --------- | ------------------ | ----- |
| dec(x) | Decimal part of a number | dec(expression) |
| sdec(x) | Signed decimal part of a number | sdec(expression) |
| ceil(x) | Ceil: Get the lowest nearest integer. Example: Numbers from 6.0 to 6.999... give 6. | ceil(expression) |
| floor(x) | Floor: Get the greatest nearest integer. Example: Numbers from 6.000...1 to 7 give 7. | floor(expression) |
| round(x) | Round: Get the nearest integer. Example: Numbers from 4.5 to 5.4999... give 5 | round(expression) |
| round(x, precision) | Round a number with a specified precision | round(expression, expressionPrecision) |

Statitics functions:

| Shortname | Name & Description | Usage |
| --------- | ------------------ | ----- |
| binc(k, n) | Binomial coefficient "n choose k" | binc(expK, expN) |

## Constants

You can find bellow all implemented constants:

| Shortname | Name & Value |
| --------- | ------------------ |
| pi | PI constant : 3.1415926535897931 |
| tau | Tau : 2 * PI |
| pau | Pau : 1.5 * PI |
| e | Euler constant : 2.7182818284590451 |
| phi | The golden ration : 1.6180339887498949 |

## Use variables in expressions

Variables can be used in expression.

A variable name can not start with a number. Examples of valid name: x, _x, x2, alpha_2

If you use a number before, it will be used a factor: `cos(2x)` -> `cos(2*x)`.

To evaluate an expression that depends on variables, we must give the used variables else a `NotDefinedException` will be throw.

## Permanent variables and expressions

We allow the user to add content that can be used at any moment thanks to `MathEvaluator.VariablesManager` and `MathEvaluator.ExpressionsManager`.

For instance, the user can create a permanent variable called `a` and defined to 5 and use it later in an expression `a+2` that will give 7.

It also works with expression. All variables and expressions must have a unique name.

We can create an expression called `f` as `x+2`. Now the function f(x) can be used anytime. We can use it as `f(2)` that will provide 4 - `f(f(2))` will provide 6.

We can create constant expression like: name: `c` - expression: `5+3`. To call it, use `c()` that will always provide 7.

We can also use different functions as bellow:

`f(x) = x^2`

`g(x, y) = x + y`

And now evaluate `f(g(3,5))` that will provide 64 (since 3 + 5 = 8 and 8^2 = 64).

We can also evaluate `f(g(2, x))` since we provide the value of x.

Also: define `u` as `f(g(x, x+1) + 1)` and then `u(5)` will provide 121 (since g(5, 6) + 1 = (5+6)+1 = 12 and f(12) = 12^2 = 144).

## Sum, Product

We allow the user to perform complex operation as the sum, the product of an expression.

Use "sum" or "prod" to use these functions.

The way to use them is always like that: `func`(`expression`, `variable_name`, `from`, `to`, `step`)

The `step` parameter is optional and is defined to 1 by default so you can use: `func`(`expression`, `variable_name`, `from`, `to`)

The `expression` can be constant or can depend of a variable. Depending on the case you can provide the name of the variable thanks to `variable_name`.
If the expression does not depend of any variable, you can provide '_' as a the variable name.

The `from` paramter must be lower than `to` parameter, else it will simply return 0.

If the `step` parameter is evaluated is lower than 0.000001, an error will be throwed.

### Examples

`sum(n, n, 1, 10)` is the sum of n from 1 to 10 with a step of 1: `1+2+...+10`. It returns 55.

`sum(1, n, 1, 10)` is the sum of 1 from 1 to 10 with a step of 1: `1+1+...+1` 10 times. The variable name is `n` but it not used. It could be write as `sum(1, _, 1, 10)`. It returns 10.

`sum(n^2, n, 1, 10)` is the sum of n^2 from 1 to 10 with a step of 1: `1+4+9+...+100`. It returns 385.

`sum(n, n, 1, 10, 2)` is the sum of n from 1 to 10 with a step of 2: `1+3+5+7+9`. It returns 25.

`sum(n, n, 0, 10, 2)` is the sum of n from 0 to 10 with a step of 2: `0+2+4+6+8+10`. It returns 30.

You can as well use variables / expressions to define parameters :

`sum(n, n, 1, a)` is the sum of n from 1 to `a` with a step of 1: `1+2+...+a`.

`sum(n, n, 1, n)` is the sum of n from 1 to `n` with a step of 1: `1+2+...+n`. 

In this case, `n` is used as the variable in the `expression` and also define the parameter `to`.
`n` must be defined previously to evaluate the parameter `to`, then `n` will evolve from 1 to `n` and it will not impact the `expression`.

`sum(n, n, 1, 2, .1)` is the sum from 1 to 2 with a step of 0.1: `1+1.1+...+1.9+2`. It return 16.5

So if  `n` = 10, `sum(n, n, 1, n)` is like `sum(n, n, 1, 10)`.

All these examples are similar for the product:

`prod(n, n, 1, 5)` is the product of n from 1 to 10 with a step of 1: `1*2*...*5`. It returns 120.

`prod(n, n, 1, 2, .2)` is the sum from 1 to 2 with a step of 0.1: `1*1.2*1.4*1.6*1.8*2`. It return 16.5

As well, you can use function inside:
`f` defined as `x`: so f(x) = x

`sum(f(n), n, 1, 10)` is like `sum(n, n, 1, 10)` that returns 55.

`prod(f(n), n, 1, 10)` is like `prod(n, n, 1, 10)` that returns 3628800. 


## Integral

We allow the user to perform complex operation as the integral of an expression.

The usage is similar to the sum / product operator.

Use: `int`(`expression`, `variable_name`, `from`, `to`, `step`)

The `step` is optional and by default, it is set to `0.1`.

### Example

`int(1, _, 0, 5)` = 5

`int(x, x, 0, 1)` = 0.5

`f(x) = x^2` and `int(f(x), x, 0, 1, 0.1)` = 0.335

`f(x) = x^2` and `int(f(x), x, 0, 1, 0.001)` = 0.333335
