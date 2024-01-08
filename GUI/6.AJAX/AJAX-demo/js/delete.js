fetch('https://jsonplaceholder.typicode.com/posts/1', {
  method: 'DELETE',
})
.then(
    console.log("Deleted")
)
