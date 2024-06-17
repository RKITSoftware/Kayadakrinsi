import { changeHandler, contentReadyHandler, copyHandler, cutHandler, disposeHandler, enterKeyHandler, focusInHandler, focusOutHandler, initializedHandler, inputHandler, keyDownHandler, keyUpHandler, keyPressHandler, optionChangedHandler, pasteHandler, valueChangedHandler } from "../event.js";

$(() => {

    DevExpress.ui.dxNumberBox.defaultOptions({
        device: { deviceType: "desktop" },
        options: {
            width: "60vw",
            height: 35,
            onInitialized: initializedHandler,
            onContentReady: contentReadyHandler,
            onCopy: copyHandler,
            onCut: cutHandler,
            onInput: inputHandler,
            onKeyDown: keyDownHandler,
            onKeyUp: keyUpHandler,
            onKeyPress: keyPressHandler,
            onPaste: pasteHandler,
        }
    });


    const simpleNumber = $("#simpleNumber").dxNumberBox({
        accessKey: "h",
        activeStateEnabled: false,
        focusStateEnabled: false,
        hoverStateEnabled: false,
        invalidValueMessage: "Enter valid number",
        isValid: true,
        mode: "tel",
        name: "simpleNumber",
        placeholder: "Enter number",
        validationStatus: "valid",
        // readOnly: true,
        // rtlEnabled:true,
        // visible: false,
        format: "#0%",
        format: "#.#",
        format: '#,##0.##',
        format: '#,##0.##;(#,##0.##)',
        value: 0.20,
        //format: {
        //    type: "decimal",
        //    precision: 2
        //}
    }).dxNumberBox("instance");


    const numbersWithButtons = $("#numbersWithButtons").dxNumberBox({
        tabIndex: 1,
        showSpinButtons: true,
        stylingMode: "filled",
        step: 101,
        text: "Random number",
        onValueChanged() {
            console.log(numbersWithButtons.option("text"), typeof (numbersWithButtons.option("text")));
            console.log(numbersWithButtons.option("value"), typeof (numbersWithButtons.option("value")));
        },
        valueChangeEvent: "keyup",
        valueChangeEvent: "focusout",
        useLargeSpinButtons: true,
    }).dxNumberBox("instance");


    const numberDisabled = $("#numberDisabled").dxNumberBox({
        showSpinButtons: true,
        showClearButton: true,
        disabled: true,
    }).dxNumberBox("instance");


    const minAndMaxNumber = $("#minAndMaxNumber").dxNumberBox({
        tabIndex: 2,
        min: 8,
        max: 90,
        value: 8,
        hint: "Range 8-90",
        showSpinButtons: true,
        showClearButton: true,
        buttons: [{
            name: "minValue",
            location: "before",
            options: {
                icon: "minus",
                stylingMode: 'outlined',
                onClick() {
                    var minValue = minAndMaxNumber.option("min");
                    minAndMaxNumber.option("value", minValue)
                }
            },
        }, {
            name: "maxValue",
            location: "after",
            options: {
                icon: "plus",
                stylingMode: 'outlined',
                onClick() {
                    var maxValue = minAndMaxNumber.option("max");
                    minAndMaxNumber.option("value", maxValue)
                }
            },
        }, {
            name: "dispose",
            location: "after",
            options: {
                icon: "trash",
                stylingMode: 'outlined',
                onClick() {
                    minAndMaxNumber.dispose();
                }
            },
        }, 'clear'],
        onDisposing: function (e) {
            var element = minAndMaxNumber.element();
            var instance = DevExpress.ui.dxNumberBox.getInstance(element);
            console.log("Element before dispose : ", element);
            console.log("Instance before dispose : ", instance);
            disposeHandler(e);
            setTimeout(() => {
                alert("Repainting min and max value number box.");
                minAndMaxNumber.repaint();
                element = minAndMaxNumber.element();
                instance = DevExpress.ui.dxNumberBox.getInstance(element);
                console.log("Element repainted : ", element);
                console.log("Instance repainted : ", instance);
                minAndMaxNumber.focus();
            }, 5000);
        },
    }).dxNumberBox("instance");

    minAndMaxNumber.on("disposing", disposeHandler);

    console.log("Dispose button : ", minAndMaxNumber.getButton("dispose"));
    console.log("MinMax instance : ", minAndMaxNumber.instance());


    const eventsNumber = $("#eventsNumber").dxNumberBox({
        showSpinButtons: true,
        elementAttr: {
            class: "element-attr-class"
        },
        inputAttr: {
            class: "input-attr-class"
        },
        format: "thousands",
        format: "currency",
        format: {
            type: "currency", // one of the predefined formats
            precision: 2, // the precision of values
            currency: "INR"
        },
        onCut: function (e) {
            console.log("cut from onCut");
        },
    }).dxNumberBox("instance");

    eventsNumber.on({
        "change": changeHandler,
        "contentReady": contentReadyHandler,
        //"copy": copyHandler,
        "cut": cutHandler,
        "disposing": disposeHandler,
        "enterKey": enterKeyHandler,
        "focusIn": focusInHandler,
        "focusOut": focusOutHandler,
        "initialized": initializedHandler,
        //"input": inputHandler,
        //"keyDown": keyDownHandler,
        //"keyUp": keyUpHandler,
        //"keyPress": keyPressHandler,
        "optionChanged": optionChangedHandler,
        //"paste": pasteHandler,
        "valueChanged": valueChangedHandler
    });

    eventsNumber.off("cut", cutHandler);
});