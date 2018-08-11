#lang=default#

var me = new string("valentin")

print(me)

##
function fibonacci(n)
    if (n <= 1)
        return n
    else
        return fibonacci(n-1) + fibonacci(n-2)
    end
end

var i = 1

while(i < 16)
    print(fibonacci(i))
    i = i + 1
end
##