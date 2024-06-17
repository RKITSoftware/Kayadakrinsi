import { contentReadyHandler, copyHandler, cutHandler, disposeHandler, enterKeyHandler, focusInHandler, focusOutHandler, initializedHandler, inputHandler, keyDownHandler, keyUpHandler, keyPressHandler, optionChangedHandler, pasteHandler, valueChangedHandler } from "../event.js";

$(function () {

    const textValue = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

    DevExpress.ui.dxTextArea.defaultOptions({
        device: { deviceType: "desktop" },
        options: {
            width: "70vw",
            //height: 60,
            accessKey: "K",
            value: textValue,
            spellcheck: false,
            placeholder: "Enter text here.",
        }
    });


    const simpleTextArea = $("#simpleTextArea").dxTextArea({
        spellcheck: true,
        hint: "simpleTextArea",
        text: "Simple TextArea",
        activeStateEnabled: false,
        focusStateEnabled: false,
        hoverStateEnabled: false,
    }).dxTextArea("instance");


    const autoResizeTextArea = $("#autoResizeTextArea").dxTextArea({
        //minHeight: 20,
        //maxHeight:60,
        autoResizeEnabled: true,
        hint: "autoResizeTextArea",
        maxLength: 2000,
    }).dxTextArea("instance");


    const readOnlyTextArea = $("#readOnlyTextArea").dxTextArea({
        readOnly: true,
        rtlEnabled: true,
        //visible:false,
        hint: "readOnlyTextArea",
        stylingMode: "filled",
        tabIndex: 1,
        minHeight: 20,
        maxHeight: 60,
    }).dxTextArea("instance");


    const eventsTextArea = $("#eventsTextArea").dxTextArea({
        onInitialized:initializedHandler,
        onContentReady: contentReadyHandler,
        onCopy: copyHandler,
        onCut: cutHandler,
        onInput: inputHandler,
        onKeyDown: keyDownHandler,
        onKeyUp: keyUpHandler,
        onKeyPress: keyPressHandler,
        onPaste: pasteHandler,
        onValueChanged: function (e) {
            valueChangedHandler(e);
            var element = eventsTextArea.element();
            console.log("Element : ", element);
            console.log("GetInstance : ", DevExpress.ui.dxTextArea.getInstance(element));
            console.log("Instance : ", eventsTextArea.instance());
        },
        onChange: function () {
            var text = eventsTextArea.option("value");
            disabledTextArea.option("value", text);
        },
        hint: "eventsTextArea",
        //valueChangeEvent: "keyup",
        //valueChangeEvent: "focusout",
    }).dxTextArea("instance");


    const disabledTextArea = $("#disabledTextArea").dxTextArea({
        disabled: true,
        hint: "disabledTextArea",
        isValid: true,
        validationStatus: "pending",
        elementAttr: {
            'area-label': "disabledElement",
        },
        inputAttr: {
            'area-label': "disabledInput",
        },
        name: "Disabled text area name",
    }).dxTextArea("instance");


    eventsTextArea.registerKeyHandler("del", function() {
        eventsTextArea.dispose();
        //$("#eventsTextArea").remove();
        setTimeout(() => {
            eventsTextArea.repaint();
        }, 3000);
    })

    eventsTextArea.on({
        "disposing": disposeHandler,
        "enterKey": enterKeyHandler,
        "focusIn": focusInHandler,
        "focusOut": focusOutHandler,
        "optionChanged": optionChangedHandler
    });


});