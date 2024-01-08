const heading = document.getElementById("heading");
const localPara = document.getElementById("local");
const sessionPara = document.getElementById("session");
const keyLocal = "counterLocal";
const keySession = "counterSession";

window.addEventListener("load", () => {

    const localCounter = Number(localStorage.getItem(keyLocal));
    const sessionCounter = Number(sessionStorage.getItem(keySession));

    if (!localStorage.getItem(keyLocal)) {

        let username = prompt("Enter your name");
        document.cookie = `username=${username}`;
        localStorage.setItem(keyLocal, 1);
        sessionStorage.setItem(keySession, 0);

    }
    else {

        localStorage.setItem(keyLocal, localCounter + 1);
        sessionStorage.setItem(keySession, sessionCounter + 1);

    }

    let usernameByCookie = document.cookie.split('=');
    heading.textContent = "Welcome " + usernameByCookie[1];

    localPara.textContent = "You are visiting this site total " + (localCounter + 1) + " times."
    sessionPara.textContent = "You are visiting this site " + (sessionCounter + 1) + " times in this session."
})

let urls=['https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css',
'https://cdn.jsdelivr.net/npm/jquery@3.6.4/dist/jquery.slim.min.js',
'https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js',
'https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js',
'https://i0.wp.com/www.flutterbeads.com/wp-content/uploads/2022/01/add-image-in-flutter-hero.png?fit=2850%2C1801&ssl=1']

caches.open('cache-links-img').then(cache=>{
    cache.addAll(urls).then(()=>{
        console.log("Data cached");
    })
    .catch((error)=>{
        console.log(error);
    })
})


