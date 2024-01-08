//----event bubbling / event propogation
const grandparent = document.querySelector(".grandparent");
const parents = document.querySelector(".parent");
const child = document.querySelector(".child");
child.addEventListener("click", () => {
    console.log("clicked on child")
})
parents.addEventListener("click", () => {
    console.log("clicked on parent")
})
grandparent.addEventListener("click", () => {
    console.log("clicked on grandparent")
})
document.body.addEventListener("click", () => {
    console.log("clicked on body")
})

// // //----event capturing
// child.addEventListener("click", () => {
//     console.log("captured child")
// }, true)
// parents.addEventListener("click", () => {
//     console.log("captured parent")
// }, false)
// grandparent.addEventListener("click", () => {
//     console.log("captured grandparent")
// }, true)
// document.body.addEventListener("click", () => {
//     console.log("captured body")
// }, true)

//----event delegation
// grandparent.addEventListener("click", (e) => {
//     console.log(e.target,e.target.textContent)
// })