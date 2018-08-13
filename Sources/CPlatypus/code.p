#lang=default#

class human
    constructor(name)
        print(name)
    end
end

var me = new human("vfrz")

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