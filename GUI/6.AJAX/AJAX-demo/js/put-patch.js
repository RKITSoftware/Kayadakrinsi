const URL = "https://jsonplaceholder.typicode.com/posts/1";

const data = {
    id:1,
    userId: 1,
    title: "Data",
    body: "Trying methods in AJAX"
}

fetch(URL, {
    method: 'PUT',
    body: JSON.stringify({
        title:"Trying PUT"
    }),
    headers: {
        'Content-type': 'application/json; charset=UTF-8',
    },
})
    .then((response) => {
        return response.json()
    })
    .then((json) => {
        console.log("After PUT");
        console.log(json)
    })
    .catch(error => {
        console.log(error);
    })

fetch(URL, {
        method: 'PATCH',
        body: JSON.stringify(data),
        headers: {
            'Content-type': 'application/json; charset=UTF-8',
        },
})
    .then((response) => {
        return response.json()
    })
    .then((json) => {
        console.log("After PATCH");
        console.log(json)
    })
    .catch(error => {
        console.log(error);
    })