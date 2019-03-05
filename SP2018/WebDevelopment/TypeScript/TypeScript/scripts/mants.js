var s = "davids";
sayHi("david", 40);
function sayHi(name, age) {
    console.log("Hello" + name + " you are " + age + " years old");
    return "foo";
}
var Person = /** @class */ (function () {
    function Person(firstName, lastName) {
        //this.name = firstName + " " + lastName;
        this.name = firstName + " " + lastName;
    }
    Person.prototype.sayHi = function () {
        console.log("Hello " + this.name + "!!! you are " + this.age + "  years old");
    };
    return Person;
}());
var p = new Person("david", "opdendries");
p.age = 40;
p.sayHi();
//let select = document.getElementById("someid") as HTMLSelectElement;
//select
function sayHello(name) {
    console.log("hello " + name);
}
function sayHello2(name) {
    console.log("hello2 " + name);
}
function callbackexample(callback) {
    callback("david");
}
//callbackexample(sayHello);
callbackexample(sayHello2);
//# sourceMappingURL=mants.js.map