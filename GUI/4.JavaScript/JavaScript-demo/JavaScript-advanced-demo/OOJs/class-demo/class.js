const firstNameTxt=document.getElementById("firstNameTxt");
const lastNameTxt=document.getElementById("lastNameTxt");
const ageNo=document.getElementById("ageNo");
const submitBtn=document.getElementById("submitBtn");
const about=document.getElementById("about");
const isAdult=document.getElementById("isAdult");

class CreateUSer {
    constructor(firstName, lastName, age) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
    }
    about() {
        return `${this.firstName} is ${this.age} years old`;
    }
    is18() {
        return this.age >= 18;
    }
}

submitBtn.addEventListener("click",(e)=>{
    e.preventDefault();
    const user=new CreateUSer(firstNameTxt.value,lastNameTxt.value,ageNo.value);
    about.innerHTML=user.about();
    isAdult.innerHTML=user.is18();
})

