#lang=default#

class Human

	var name
	var age

	constructor(name, age)
		this.name = name
		this.age = age
	end

	function presentation()
		print("Hello, my name is " + name + "and i'm " + age + "year(s) old !")
	end

	function sayHello(anotherHuman)
		print(name + " said Hello to " + anotherHuman.name)
	end

end

var alvin = new Human("Alvin")
var valentin = new Human("Valentin")

alvin.presentation()

valentin.sayHello(alvin)