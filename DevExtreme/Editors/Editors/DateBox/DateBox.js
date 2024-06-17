// Imports event functions from 'event.js'
import { changeHandler, closedHandler, copyHandler, cutHandler, contentReadyHandler, disposeHandler, enterKeyHandler, focusInHandler, focusOutHandler, initializedHandler, inputHandler, keyDownHandler, keyUpHandler, keyPressHandler, openedHandler, optionChangedHandler, pasteHandler, valueChangedHandler } from "../event.js";

$(function () {

    // Sets defualtt options to all elements of specified type
    DevExpress.ui.dxDateBox.defaultOptions({
        device: { deviceType: "desktop" },
        options: {
            adaptivityEnabled: true,
            width: "100vw",
            height: 30,
            //value: new Date(2024, 4, 8),
        }
    });


    const dateCalendar = $("#dateCalendar").dxDateBox({

        // A format used to display date/time information.
        type: "date",

        // Specifies the type of the date/time picker.
        pickerType: "calendar",

        accessKey: "c",

        // Specifies the way an end- user applies the selected value. (instantly | useButtons)
        applyValueMode: "useButtons",

        // Configures the calendar's value picker. Applies only if the pickerType is "calendar".
        calendarOptions: {
            text: "Hello",
            useMaskBehavior: true,
            displayFormat: "yyyy",
            cellTemplate: function (date, text, view) {
                //console.log("Date" + date + "Text" + text + "View" + view);
                text = "k";
                //console.log(date);
                view.append("<div>" + date.text + text + "</div>").css("font-weight", "bold");
            }
        },

        focusStateEnabled: false,
        hoverStateEnabled: false,
        maxLength: 5,
        placeholder: "Please select date",

    }).dxDateBox("instance");


    const dateList = $("#dateList").dxDateBox({

        type: "date",
        pickerType: "list",
        activeStateEnabled: false,
        applyValueMode: "instantly", // defualt

        // Specifies dates that users cannot select.Applies only if pickerType is "calendar".
        disabledDates: [
            new Date(2024, 0, 8),
            new Date(2024, 1, 8),
            new Date(2024, 2, 8)
        ],

        // Specifies the message displayed if the typed value is not a valid date or time.
        invalidDateMessage: "Enter valid date",

        // Specifies the interval between neighboring values in the popup list in minutes. Only applicable for type : time and picker-type : list
        interval: 10,

        // Specifies the maximum number of characters you can enter into the textbox.
        maxLength: 5,

    }).dxDateBox("instance");


    const dateNative = $("#dateNative").dxDateBox({

        type: "date",
        pickerType: "native",
        interval: 10,

        // The minimum date that can be selected within the UI component.
        min: new Date(2024, 3, 1),

        // The last date that can be selected within the UI component.
        max: new Date(2024, 3, 30),

        // Specifies the message displayed if the specified date is later than the max value or earlier than the min value.
        dateOutOfRangeMessage: "Date is out of range",

        // Specifies the date-time value serialization format. Use it only if you do not specify the value at design time.
        dateSerializationFormat: "yyyy-MM-dd", // a local date
        dateSerializationFormat: "yyyy-MM-ddTHH:mm:ss", // local date and time
        dateSerializationFormat: "yyyy-MM-ddTHH:mm:ssZ", // the UTC date and time
        dateSerializationFormat: "yyyy-MM-ddTHH:mm:ssx", // date and time with a timezone

        // A function that is executed after the UI component's value is changed.
        onValueChanged() {
            console.log(e.value);
        }

    }).dxDateBox("instance");


    const dateRollers = $("#dateRollers").dxDateBox({

        type: "date",
        pickerType: "rollers",

        // Specifies a placeholder for the input field.
        placeholder: "Please select date",

        // The text displayed on the Apply button.
        applyButtonText: "Final",

        // The text displayed on the Cancel button.
        cancelButtonText: "Discard",

        // Specifies whether to render the drop-down field's content when it is displayed. If false, the content is rendered immediately. Defualt true
        deferRendering: false,

        // Specifies whether to control user input using a mask created based on the displayFormat. Defualt false
        useMaskBehavior: true,

        // Specifies the date display format. Ignored if the pickerType property is "native"
        displayFormat: 'shortdate',
        displayFormat: 'EEEE, d of MMM, yyyy HH:mm',
        displayFormat: "'Year': yyyy",

        disabledDates: [
            new Date("07/1/2024"),
            new Date("07/2/2024"),
            new Date("07/3/2024"),
        ],
        invalidDateMessage: "Enter valid date",
        onValueChanged: function (e) {
            valueChangedHandler(e);
        },

    }).dxDateBox("instance");

    // Sets focus to the input element representing the UI component.
    dateRollers.focus();

    setTimeout(() => {
        // Removes focus from the input element.
        dateRollers.blur();
    }, 6000);


    const timeCalendar = $("#timeCalendar").dxDateBox({

        // A function used in JavaScript frameworks to save the UI component instance.
        onInitialized: initializedHandler(),

        // A function that is executed when the UI component's content is ready and each time the content is changed.
        onContentReady: contentReadyHandler(),

    }).dxDateBox("instance");

    // Updates the values of several properties.
    timeCalendar.option({

        type: "time",
        maxLength: 5,
        pickerType: "list",
        deferRendering: false,
        acceptCustomValue:true,

        // Specifies the global attributes to be attached to the UI component's container element.
        elementAttr: {
            class: "timeCalendar",
        },

        // Indicates or specifies the current validation status. (invalid | valid | pending)
        validationStatus: "invalid",

        // Specifies text for a hint that appears when a user pauses on the UI component.
        hint: "Select time",

        // Specifies whether or not the drop-down editor is displayed.
        opened: true,

        // Specifies whether a user can open the drop-down list by clicking a text field.
        openOnFieldClick: true,

        // Specifies whether the drop-down button is visible.
        showDropDownButton: false,

        // pecifies the number of the element when the Tab key is used for navigating.
        tabIndex: 1,

        // Specifies whether or not the UI component checks the inner text for spelling mistakes.
        spellcheck: true,

        // Specifies how the UI component's text field is styled. Defualt outlined
        stylingMode: "filled",
        stylingMode: "outlined",
        stylingMode: "underlined",

        interval: 1,

        // The read-only property that holds the text displayed by the UI component input element.
        text: "Hello",

        // A function that is executed when the UI component loses focus after the text field's content was changed using the keyboard.
        onChange: function (e) {
            changeHandler(e);
        },

        // A function that is executed when the UI component's input has been cut.
        onCut: function (e) {
            cutHandler(e);
        },

        // A function that is executed when the UI component's input has been copied.
        onCopy: function (e) {
            copyHandler(e);
        },

        // A function that is executed when the UI component's input has been pasted.
        onPaste: function (e) {
            pasteHandler(e);
        },

        // A function that is executed when a user is pressing a key on the keyboard.
        onKeyDown: function (e) {
            keyDownHandler(e);
        },

        // A function that is executed when a user releases a key on the keyboard.
        onKeyUp: function (e) {
            keyUpHandler(e);
        },

        // A function that is executed when a user presses a key on the keyboard.
        onKeyPress: function (e) {
            keyPressHandler(e);
        },

        // A function that is executed each time the UI component's input is changed while the UI component is focused.
        onInput: function (e) {
            inputHandler(e);
        },

        // A function that is executed after the UI component's value is changed.
        onValueChanged: function (e) {
            valueChangedHandler(e);
            console.log("Field", timeCalendar.field());
            setTimeout(() => {
                // Disposes of all the resources allocated to the DateBox instance.
                timeCalendar.dispose();
                console.log("Field after dispose", timeCalendar.field());
            }, 3000);
        },

        valueChangeEvent:"keyup",
    });

    setTimeout(() => {
        // Closes the drop-down editor.
        timeCalendar.close();

        // Gets the popup window's content.
        console.log("POPup close", timeCalendar.content());

        // Gets the UI component's <input> element.
        console.log("Field", timeCalendar.field());
    }, 3000);

    setTimeout(() => {
        // Opens the drop-down editor.
        timeCalendar.open();
        console.log("POPup open", timeCalendar.content());
        console.log("Field", timeCalendar.field());
    }, 7000);


    const timeList = $("#timeList").dxDateBox({

        type: "time",
        pickerType: "list",

        // Specifies whether the UI component responds to user interaction.
        disabled: true,

        // Specifies or indicates whether the editor's value is valid. Defualt true
        isValid: false,
        validationStatus: "valid",
        height: "5vh",
        width: "10vw",
        value: new Date(2024, 4, 8),
        hint: "Disabled", // Not working becuase disabled

        //buttons: [{
        //    location: "before",
        //    name: "beforeButton",
        //    options: {
        //        icon: "edit",
        //        stylingMode: 'text',
        //        onClick() {
        //            alert("Before Icon clicked");
        //        }
        //    },
        //},
        //{
        //    location: "after",
        //    name: "afterButton",
        //    options: {
        //        icon: "edit",
        //        stylingMode: 'text',
        //        onClick() {
        //            alert("After Icon clicked");
        //        }
        //    },
        //}],


    }).dxDateBox("instance");


    const timeNative = $("#timeNative").dxDateBox({

        type: "time",
        pickerType: "native",

    }).dxDateBox("instance");


    const timeRollers = $("#timeRollers").dxDateBox({

        type: "time",
        pickerType: "rollers",

        // Specifies a custom template for the drop-down button.
        dropDownButtonTemplate: function (icon, text) {
            icon = "chevrondown"; // The button's icon.
            text.prepend(`<span class="dx-icon-${icon}"></span>`); // The button's text.
        },

        // Configures the drop-down field which holds the content. (POPUP configurations)
        dropDownOptions: {
            maxHeight: "45vh",
            shadingColor: "rgba(0,0,0,0.8)",
            //hideOnOutsideClick:true,
            //disabled: true,
        }

    }).dxDateBox("instance");


    const datetimeCalendar = $("#datetimeCalendar").dxDateBox({

        type: "datetime",
        maxLength: 5,
        pickerType: "calendar",

        // Specifies whether to show the analog clock in the value picker. Applies only if type is "datetime" and pickerType is "calendar".
        showAnalogClock: false,

        // Specifies whether to display the Clear button in the UI component.
        showClearButton: true,

        // Specifies whether or not the UI component allows an end-user to enter a custom value.
        //acceptCustomValue: false, // defualt true
        interval: 1,

    }).dxDateBox("instance");


    const datetimeList = $("#datetimeList").dxDateBox({

        type: "datetime",
        pickerType: "list",

        // Allows you to add custom buttons to the input text field.
        buttons: [{
            location: "before",
            name: "beforeButton",
            options: {
                icon: "edit",
                stylingMode: 'text',
                onClick() {
                    alert("Before Icon clicked");
                }
            },
        },
        {
            location: "after",
            name: "afterButton",
            options: {
                icon: "trash",
                stylingMode: 'text',
                onClick() {
                    datetimeList.option("value", "");
                    /*alert("After Icon clicked");*/
                }
            },
        }, 'dropDown'],

        // Specifies the attributes to be passed on to the underlying HTML element.
        inputAttr: {
            id: "dateTimeListInput",
            type: "text",
            //pattern: "\\d{2}-\\d{2}-\\d{4}",
        },

        // Specifies the global attributes to be attached to the UI component's container element.
        elementAttr: {
            class: "dateTimeList",
            id: "dateTimeList",
        },

        // The value to be assigned to the name attribute of the underlying HTML element.
        name: "DateTimeList",
        interval: 10,

    }).dxDateBox("instance");

    setTimeout(() => {
        // Gets an instance of a custom action button.
        console.log("Before button", datetimeList.getButton("beforeButton"));
        console.log("After button", datetimeList.getButton("afterButton"));
    }, 2000);


    const datetimeNative = $("#datetimeNative").dxDateBox({

        type: "datetime",
        pickerType: "native",

        // Specifies whether the editor is read-only.
        readOnly: true,

        // Switches the UI component to a right-to-left representation.
        rtlEnabled: true,
        interval: 10,

    }).dxDateBox("instance");


    const datetimeRollers = $("#datetimeRollers").dxDateBox({

        type: "datetime",
        pickerType: "rollers",

        // Specifies whether the UI component is visible.
        visible: false,

    }).dxDateBox("instance");

    // Subscribes to events.
    timeCalendar.on({
        "closed": closedHandler,
        "disposing": disposeHandler,
        "enterKey": enterKeyHandler,
        "focusIn": focusInHandler,
        "focusOut": focusOutHandler,
        "opened": openedHandler,
        "optionChanged": optionChangedHandler,
    });

});
