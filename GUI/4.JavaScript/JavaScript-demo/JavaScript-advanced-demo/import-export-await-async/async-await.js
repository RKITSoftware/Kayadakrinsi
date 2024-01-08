console.log("Start")

const URL="https://jsonplaceholde.typicode.com/posts";

// fetch(URL)
//     .then(response=>{
//         return response.json();
//     })
//     .then(data=>{
//         console.log(data);
//     })

async function getPosts(){
    const response=await fetch(URL);
    console.log(response);
    if(!response.ok){
        throw new Error("Something is wrong!!");
    }
    const data=await response.json();
    return data;
}

// const getPosts=async()=>{
//     const response=await fetch(URL);
//     if(!response.ok){
//         throw new Error("Something is wrong!!");
//     }
//     const data=await response.json();
//     return data;
// }

// const returned = getPosts();
// console.log(returned); //op:promise

getPosts()
    .then(myData=>{
        console.log(myData);
    })
    .catch(error=>{
        console.log(error);
    })

console.log("end");