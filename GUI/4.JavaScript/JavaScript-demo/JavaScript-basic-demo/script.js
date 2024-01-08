const firstName=document.querySelector('firstName');
const email = document.getElementById('email');
const form = document.getElementById('regForm');
const dateOfBirth = document.getElementById('dateOfBirth');
const address = document.getElementById('address');

form.addEventListener("submit", function (e) {

    e.preventDefault();
   
    let pattern_email = /[a-z0-9._%+-]+@+[a-z0-9._]+\.[a-z]{2,4}$/;

    if (!pattern_email.test(email.value)) {
        alert('Invalid Email !');
        console.log('hoioo');
    }

    alert('ok');
});

const d = new Date();
let date = d.getDate();
let month = d.getMonth() + 1;
let year = d.getUTCFullYear();
if (date < 10) {
    date = '0' + date;
}
if (month < 10) {
    month = '0' + month;
}
let maxDate = year + "-" + month + "-" + date;

dateOfBirth.addEventListener("click", () => {
    dateOfBirth.setAttribute("max", maxDate);
})

dateOfBirth.addEventListener("focusout", () => {
        if (dateOfBirth.value>maxDate){
            alert('enter valid date');
            return 0;
        }
    return 1;
})

dateOfBirth.addEventListener("mouseover", function (e) {
    e.target.style.color = "red";
});

dateOfBirth.addEventListener("mouseout", function (e) {
    e.target.style.color = "black";
});

window.addEventListener("scroll", function () {
    console.log('scrolling');
})

address.addEventListener("keydown", function (e) {
    e.target.style.backgroundColor = "lightblue";
})

address.addEventListener("keypress", function (e) {
    e.target.style.backgroundColor = "violet";
})

address.addEventListener("keyup", function (e) {
    e.target.style.backgroundColor = "white";
})

