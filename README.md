# CPlatypus

**Platypus** is a non-typed, object-oriented, open-source, grammar-customizable, easy-to-learn, interpreted programming language!

**CPlatypus** is an official interpreter written in C# and running on .Net Core

## Code samples

**Hello world**
```
#lang=default#

print("Hello world")
```

**Fibonacci**

```
#lang=default#

function fibonacci(n)
    if (n <= 1)
        return n
    else
        return fibonacci(n-1) + fibonacci(n-2)
    end
end

var i = 0

while(i < 16)
    print(fibonacci(i))
    i = i + 1
end
```
