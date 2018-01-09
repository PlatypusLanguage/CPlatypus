#lang=default#

function test()
    return 20 + 4
end

var a = new Integer(new Integer(test()))

print(a)