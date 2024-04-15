# SI Calculator

This is a simple calculator that calculates SI UNITS

7 km / h = 1.944 m / s
7kg * 9.8 m / s ^ 2 = 68.6 N
5N / 2m = 2.5 N / m
7N * 2m = 14 J
7N * 2m / 2s = 7 W
2m^2 * 4cd = 8 lm
m^2 * cd = lm

s
m

kg
A
K
mol
cd

N = kg * m / s^2
J = m * N = kg * m^2 / s^2
W = J / s = kg * m^2 / s^3
V = W / A = kg * m^2 / s^3 / A = kg * m^2 / s^3 / A
Ohm = V / A = kg * m^2 / s^3 / A / A = kg * m^2 / s^3 / A^2

Expand / Simplify commands
Calculator using units
Calculator with units as a safety measure

2 km / h * 10 s = 2000 m / 3600 s * 10 s = 5.56 m

ideas:
expand(N) = kg * m / s^2
simplify(kg * m / s^2) = N

convertUnit(km, m) = 1000 m

calculate(7 km / h) = 1.944 m / s
convert(8m/s, km/h) = 28.8 km/h

2 cups = 1 pint = 0.473176 L
2 pints = 1 quart = 0.946353 L
4 quarts = 1 gallon = 3.78541 L

## First round

2m / 3s = 0.6667 m / s

Tokens: m, s, km, h
Units: m, s
Operations: +, -, *, /

### Grammar

Number = [0-9.]+
Unit = m | s | km | h
Scalar = Number + Unit
Operation = + | - | * | /
Constant = Number Unit

## Inspiration

https://tyrrrz.me/blog/expression-trees
https://tyrrrz.me/blog/monadic-parser-combinators
https://github.com/datalust/superpower
https://www.nist.gov/pml/owm/metric-si/si-units
