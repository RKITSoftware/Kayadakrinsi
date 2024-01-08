const clickBtn=document.getElementById("clickBtn");

function mouseOvered(){
    console.log("Mouse Overed Inline");
}

clickBtn.onmouseover=function(){
    console.log("Mouse Overed onmouseover");
}

$("#clickBtn").mouseover(function(){
    console.log("Mouse Overed without document ready")
})


clickBtn.addEventListener('mouseover',function(){
    console.log("Mouse Overed addEventlistner");
})

$(document).ready(function(){
    $("#clickBtn").mouseover(function(){
        console.log("Mouse Overed document ready")
    })
})

// $("#clickBtn").mouseover(function(){
//     console.log("Mouse Overed without document ready")
// })