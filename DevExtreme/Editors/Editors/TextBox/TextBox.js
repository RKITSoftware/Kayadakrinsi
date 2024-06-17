import { changeHandler, contentReadyHandler, copyHandler, cutHandler, disposeHandler, enterKeyHandler, focusInHandler, focusOutHandler, initializedHandler, inputHandler, keyDownHandler, keyPressHandler, keyUpHandler, optionChangedHandler, pasteHandler, valueChangedHandler } from "../event.js";

$(() => {

    DevExpress.ui.dxTextBox.defaultOptions({
        device: { deviceType: "desktop" },
        options: {
            height: 40,
            width: "70vw",
            placeholder: "Enter text",
            value: "Hello world",
            elementAttr: {
                'area-label': "textBox-element"
            },
            inputAttr: {
                'area-label': "textBox-input"
            },
            maxLength: 1000,
        }
    });


    const simpleTextBox = $("#simpleTextBox").dxTextBox({
        accessKey: "K",
        activeStateEnabled: false,
        focusStateEnabled: false,
        hoverStateEnabled: false,
        spellcheck: false,
        showClearButton: true,
        hint: "Simple TextBox"
    }).dxTextBox("instance");


    const readOnlyTextBox = $("#readOnlyTextBox").dxTextBox({
        readOnly: true,
        visible: false,
        visible: true,
        tabIndex: 1,
        isValid: true,
        validationStatus: "pending",
        stylingMode: "underlined",
        hint: "Read only TextBox",
    }).dxTextBox("instance");


    const disabledTextBox = $("#disabledTextBox").dxTextBox({
        disabled: true,
        rtlEnabled: true,
        name: "disabledName",
        text: "disabledText",
        hint: "Disabled TextBox",
    }).dxTextBox("instance");


    const maskTextBox = $("#maskTextBox").dxTextBox({
        text: "maskTextBox",
        hint: "Masked TextBox",
        value: "",
        mask: "LL-00-KK-0000",
        maskInvalidMessage: "Value is not in correct format",
        maskChar: "#",
        maskRules: {
            K: /[A-Z]/,
        },
        useMaskedValue: true,
        showMaskMode: "onFocus",
        onValueChanged() {
            console.log(maskTextBox.option("value"));
        },
    }).dxTextBox("instance");


    const modeTextBox = $("#modeTextBox").dxTextBox({
        text: "TextBox modes",
        hint: "TextBox modes",
        value: "",
        mode: "email",
        mode: "tel",
        mode: "text",
        mode: "url",
        mode: "password",
        mode: "search",
        buttons: [{
            name: "offClick",
            location: "after",
            options: {
                icon: "revert",
                onClick: function () {
                    modeTextBox.option("onCopy", "");
                    alert("Copy enabled.");
                }
            }
        }],
        onCopy: function (e) {
            e.event.preventDefault();
            alert("Can't copy!");
        },
    }).dxTextBox("instance");


    const eventsTextBox = $("#eventsTextBox").dxTextBox({
        onInitialized: initializedHandler,
        onContentReady: contentReadyHandler,
        hint: "Events TextBox",
        valueChangeEvent: "focusout",
        buttons: [{
            name: "disposeButton",
            location: "after",
            options: {
                icon: "trash",
                onClick: function () {
                    eventsTextBox.dispose();
                    setTimeout(() => {
                        eventsTextBox.repaint();
                    }, 3000);
                }
            }
        }],
        onChange: changeHandler,
        onCopy: copyHandler,
        onCut: cutHandler,
        onInput: inputHandler,
        onKeyDown: keyDownHandler,
        onKeyPress: keyPressHandler,
        onKeyUp: keyUpHandler,
        onPaste: pasteHandler,
    }).dxTextBox("instance");

    eventsTextBox.on({
        "disposing": function (e) {
            disposeHandler(e);
            eventsTextBox.off("optionChanged", optionChangedHandler);
        },
        "enterKey": enterKeyHandler,
        "focusIn": focusInHandler,
        "focusOut": focusOutHandler,
        "optionChanged": optionChangedHandler,
        "valueChanged": function (e) {
            valueChangedHandler(e);
            var element = eventsTextBox.element();
            console.log("Element : ", element);
            console.log("GetInstance : ", DevExpress.ui.dxTextBox.getInstance(element));
            console.log("Instance : ", eventsTextBox.instance());
            console.log("Button : ", eventsTextBox.getButton("disposeButton"));
        },
    });

});