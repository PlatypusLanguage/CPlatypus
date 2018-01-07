#lang=default#

function test() 
    function hello()
        return "Hello, " + readname() + " !"
        function readname()
            return read()
        end
    end
    print("Enter your name :")
    return hello()
end

print(test())