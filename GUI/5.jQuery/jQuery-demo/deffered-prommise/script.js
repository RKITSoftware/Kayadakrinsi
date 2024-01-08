$(document).ready(function () {

    var def = $.Deferred();

    def.done(val => {
        console.log(val);
    })

    def.fail(val => {
        console.log(val);
    })
    
    console.log(def.state());
    
    $("#resolvebtn").click(function () {
        def.resolve("Differed object resolved");
        console.log(def.promise());
        console.log(def.state());
    })

    $("#rejectbtn").click(function () {
        def.reject("sorry");
        console.log(def.state());
        console.log(def.promise());
    })
})
