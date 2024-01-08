class Animal {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }
    eat() {
        console.log(this.name , " is eating");
        console.log(`${this.name} is eating`);
    }
    // eat(name,age){
    //     console.log("name : ");
    // }
    isSuperCute() {
        return this.age <= 1;
    }
}

const animal1 = new Animal('Dog', 2);
console.log(animal1);
animal1.eat();
animal1.eat("dog",1);

class Dog extends Animal {
    constructor(name, age, speed) {
        super(name, age);
        this.speed = speed;
    }
    eat(name,age){
        console.log("eat from dog...")
    }
    run() {
        return `${this.name}'s speed is ${this.speed} kmph`;
    }
}
const doggo = new Dog('doggo', 1, 34);
console.log(doggo, doggo.isSuperCute(), doggo.run());
doggo.eat();
doggo.eat("abc",1);


