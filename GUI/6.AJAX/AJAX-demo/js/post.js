const URL = "https://jsonplaceholder.typicode.com/posts";

const data = {
    userId: 1,
    title: "Post Data",
    body: "Trying post method in AJAX"
}

fetch(URL, {
    method: 'POST',
    body: JSON.stringify(data),
    headers: {
        'Content-type': 'application/json; charset=UTF-8',
    },
})
    .then((response) => {
        return response.json()
    })
    .then((json) => {
        console.log("After POST");
        console.log(json)
    })
    .catch(error => {
        console.log(error);
    })