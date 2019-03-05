var s = "davids";

sayHi("david", 40);

function  sayHi(name : string, age : number) : string {
    console.log("Hello" + name + " you are " + age + " years old");
    return "foo";
}

class Person {
    name: string;
    age: number;
    favoriteFood: string;

    constructor(firstName : string, lastName :string) {
        //this.name = firstName + " " + lastName;

        this.name = `${firstName} ${lastName}`;
    }

    public sayHi() {
        console.log(`Hello ${this.name}!!! you are ${this.age}  years old`);
    }

}


let p = new Person("david", "opdendries");
p.age = 40;
p.sayHi();

//let select = document.getElementById("someid") as HTMLSelectElement;

//select
function sayHello(name: string) {
    console.log("hello " + name);
}

function sayHello2(name: string) {
    console.log("hello2 " + name);
}

function callbackexample(callback: (a : string) => void)
{
    callback("david");
}

//callbackexample(sayHello);
callbackexample(sayHello2);






