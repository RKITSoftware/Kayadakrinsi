$(function ()
{
    // Appends button created using dxButton() to the div
    const button = $("#button").dxButton().dxButton("instance");

    // Sets several properties using option() method
    button.option({
        text: "Click me!",
        onClick: function () {
            alert("Hello world!");
        }
    });

    //$("#textBox").dxButton({
    //    text: "Click",
    //    onClick: function () {
    //        alert("Hello");
    //    }
    //});

    // Appends textbox created using dxTextBox() to the div
    var textBox = $('#textBox').dxTextBox().dxTextBox("instance");

    // Sets several properties using option() method
    textBox.option({
        value: 'Text Box',
        inputAttr: { 'aria-label': 'Name' }
    });

    // Focus stays on latest focused element
    textBox.focus();
    button.focus();

    // registers key using registerKeyHandler() method
    textBox.registerKeyHandler("tab", function () {
        textBox.option("value", "Text Box greetings");
    })

    // registers key using registerKeyHandler() method
    textBox.registerKeyHandler("enter", function () {
        alert("Disposing Text Box");
        textBox.dispose();
    })

    // Gets single property value
    var buttonText = button.option("text");

    var textValue = textBox.option("value");

    console.log("TextBox value is : " + textValue);

    // Sets single property value
    textBox.option("value", "Greetings");

    textValue = textBox.option("value");

    // Gets all properties of textBox
    var textBoxAll = textBox.option();

    console.log("Button text is : " + buttonText);
    console.log("TextBox value is : " + textBoxAll.value);
    console.log("TextBox inputAttr is : " + textBoxAll.inputAttr);

});