export const name = "Krinsi";

export const addTwoNumbers = (a, b) => {
    if (typeof a == "number" && typeof b == "number") {
        alert(`${a} + ${b} = ${Number(a) + Number(b)}`);
    }
    else {
        alert('Invalid number/s, Try again!!');
    }
} //---never add ; here

export default class Animal {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }
    eat() {
        return `${this.name} is eating`;
    }
    isSuperCute() {
        return this.age <= 1;
    }
}
// ----or
// export { name, addTwoNumbers, Animal }