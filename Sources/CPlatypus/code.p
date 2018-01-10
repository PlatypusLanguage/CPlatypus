#lang=default#

function fibonacci(n)
    if (n <= 1)
        return n
    else
        return fibonacci(n-1) + fibonacci(n-2)
    end
end

print(fibonacci(15))