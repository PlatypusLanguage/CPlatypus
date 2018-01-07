#lang=default#

function test()
    return hello()
    function hello()
        return "Hello" + world()
        function world()
            return " world !"
        end
    end
end

print(test())