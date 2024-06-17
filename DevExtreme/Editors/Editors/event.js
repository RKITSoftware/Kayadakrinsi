function changeHandler(e) {
    console.log("Changed", e);
}

function closedHandler(e) {
    console.log("Drop down closed", e);
}

function contentReadyHandler(e) {
    console.log("Content is ready", e);
}

function copyHandler(e) {
    //console.log("Copied", e);
    alert("Copied");
}

function cutHandler(e) {
    alert("Cut");
    //console.log("Cut", e);
}

function disposeHandler(e) {
    alert("Disposing", e);
}

function enterKeyHandler() {
    alert("Value will be selected");
}

function focusInHandler(e) {
    console.log("Focused in", e);
}

function focusOutHandler(e) {
    console.log("Focused out", e);
}

function initializedHandler(e) {
    console.log("Initialized", e);
}

function inputHandler(e) {
    console.log("Input recive", e);
}

function keyDownHandler(e) {
    console.log("Key down", e.event.key, e.event.keyCode);
}

function keyUpHandler(e) {
    console.log("Key up", e.event.key, e.event.keyCode);
}

function keyPressHandler(e) {
    console.log("Key press", e.event.key, e.event.keyCode);
}

function openedHandler(e) {
    console.log("Drop down opened", e);
}

function optionChangedHandler(e) {
    console.log("Option changed", e.name, e.value);
}

function valueChangedHandler(e) {
    console.log(`Value changed "${e.previousValue}" to "${e.value}"`);
}

function pasteHandler(e) {
    //console.log("Pasted ", e);
    alert("Pasted ");
}

export { changeHandler, closedHandler, copyHandler, cutHandler, contentReadyHandler, disposeHandler, enterKeyHandler, focusInHandler, focusOutHandler, initializedHandler, inputHandler, keyDownHandler, keyUpHandler, keyPressHandler, openedHandler, optionChangedHandler, pasteHandler, valueChangedHandler };