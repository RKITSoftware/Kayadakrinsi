const array1 = [1, 3, 2, 4, 5];
const array2 = [9, 10, 7, 6, 8];
var object1 = {
    apple: 0,
    banana: { weight: 52, price: 100 },
    cherry: 97
};
var object2 = {
    banana: { price: 200 },
    durian: 100
};
var objectExtra = {
    name:"Krinsi",
    age:"20"
}

$(document).ready(function () {
    // merge()
    const array3 = $.merge(array1, array2);
    console.log(array3);

    // grep()
    const evens = $.grep(array3, function (n) {
        console.log(this);
        return n % 2==0;
    })
    console.log(evens)

    // map()
    const squares = $.map(evens, function (n) {
        return n * n;
    })
    console.log(squares);

    // extend()
    const object3=$.extend(object1, object2);
    console.log(object3);
    const object4=$.extend(object1, object2,objectExtra);
    console.log(object4);

})
