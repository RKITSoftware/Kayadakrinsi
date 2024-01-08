import { name as n, addTwoNumbers} from './exportDemo.js';
const button = document.querySelector("button");

button.addEventListener("click", () => {
    
    console.log(n);

    const a = prompt("Enter first number : ");
    const b = prompt("Enter second number : ");
    addTwoNumbers(a, b);

    const dog = new Animal("doggo", 1);
    console.log(dog.age, dog.eat(), dog.isSuperCute());
})
