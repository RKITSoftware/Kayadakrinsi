// Imports event functions from 'event.js'
import { changeHandler, closedHandler, copyHandler, cutHandler, contentReadyHandler, disposeHandler, enterKeyHandler, focusInHandler, focusOutHandler, initializedHandler, inputHandler, keyDownHandler, keyUpHandler, keyPressHandler, openedHandler, optionChangedHandler, pasteHandler, valueChangedHandler } from "../event.js";


$(function () {

    // Sets defualtt options to all elements of specified type
    DevExpress.ui.dxDropDownBox.defaultOptions({
        device: { deviceType: "desktop" },
        options: {
            width: "40vw",
            height: 30,
        }
    });

    //// Define the cities array
    //const cities = [
    //    {
    //        "city": "Rajkot",
    //        "product": "Book"
    //    },
    //    {
    //        "city": "Delhi",
    //        "product": "Book"
    //    }];

    //// Create a custom data source function for storing static data
    //function makeDataSource(data) {
    //    return new DevExpress.data.DataSource({
    //        store: data
    //    });
    //}


    // Create a custom data source function for storing data from json files
    const makeAsyncDataSource = function (jsonFile) {
        return new DevExpress.data.CustomStore({
            loadMode: 'raw',
            key: 'city',
            load() {
                return $.getJSON(`${jsonFile}`);
            },
        });
    }

    // Create a custom data source function for storing data from API endpoints
    function getData(urlEndPoint) {
        return new DevExpress.data.CustomStore({
            loadMode: "raw",
            key: "response",
            load: async function (loadOptions) {
                var x = await $.ajax({
                    url: urlEndPoint,
                    method: "GET",
                    dataType: "json",
                    data: loadOptions,
                });
                console.log(x);
                console.log(x?.response);
                return x?.response;
            }
        });
    };


    const singleSelectionDropDownBox = $("#singleSelectionDropDownBox").dxDropDownBox({

        // A function used in JavaScript frameworks to save the UI component instance.
        onInitialized: initializedHandler(),

        // A function that is executed when the UI component's content is ready and each time the content is changed.
        onContentReady: contentReadyHandler(),

    }).dxDropDownBox("instance");

    // Updates the values of several properties.
    singleSelectionDropDownBox.option({

        // Specifies whether or not the UI component allows an end-user to enter a custom value. Defualt false
        acceptCustomValue: true, 
        
        accessKey: "A",
        activeStateEnabled: false,
        focusStateEnabled: false,
        value: "Gujarat",

        // Specifies which data field provides unique values to the UI component's value.
        valueExpr: 'state',

        // Specifies the data field whose values should be displayed.
        displayExpr: 'state',

        placeholder: 'Select a value...',
        showClearButton: true,

        // Configures the drop-down field which holds the content. (POPUP configurations)
        dropDownOptions: {
            showTitle: true,
            title: 'Select states',
            dragEnabled: true,
            shadingColor: "rgba(0,0,0,0.8)",
        },

        inputAttr: {
            'aria-label': 'singleSelectionDropDownBox',
            id: "input",
        },

        elementAttr: {
            class: "multiple drop down",
        },

        // Customizes text before it is displayed in the input field.
        displayValueFormatter: function (value) {
            return "State : " + value;
        },

        hint: "Select or enter state",
        hoverStateEnabled: false,
        isValid: true,
        name: "Drop down",
        maxLength: 10,

        // Specifies a custom template for the text field. Must contain the TextBox UI component.
        //fieldTemplate: function (value, fieldElement) {
        //    const result = $("<div class='custom-item'>");
        //    result
        //        .dxTextBox({
        //            value: value,
        //            readOnly: true
        //        });
        //    fieldElement.append(result);
        //},
        //readOnly:true,
        //rtlEnabled:true,

        // Binds the UI component to data.
        dataSource: makeAsyncDataSource('data.json'),

        // Specifies a custom template for the drop-down content.
        contentTemplate: function (e) {
            var $list = $("<div>").dxList({

                searchEditorOptions: {
                    placeholder: 'Enter state',
                    showClearButton: true,
                    inputAttr: { 'aria-label': 'Search' },
                },
                searchExpr: ["city"],
                searchEnabled: true,

                dataSource: e.component.getDataSource(),

                showSelectionControls: true,
                selectionMode: 'single',

                itemTemplate: function (itemData) {
                    return $("<div>").text(itemData.city);
                },
                //onSelectionChanged: function (args) {
                //    var selectedItems = args.addedItems[0];
                //    console.log(singleSelectionDropDownBox.option("value"));
                //    singleSelectionDropDownBox.option("value", selectedItems);
                //    singleSelectionDropDownBox.close();
                //},
                onItemClick: function (selectedItems) {
                    singleSelectionDropDownBox.option("value", selectedItems.itemData.state);
                    singleSelectionDropDownBox.close();
                }
            });
            return $list;
        },

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
        },
    });


    const multipleSelectionDropDownBox = $("#multipleSelectionDropDownBox").dxDropDownBox({

        acceptCustomValue: true, // Defualt false
        valueExpr: 'state',
        displayExpr: 'city',
        placeholder: 'Select a value...',
        showClearButton: true,
        //disabled: true,
        //showDropDownButton: false,
        //visible:false,
        //opened: true,
        //openOnFeildClick: true,
        inputAttr: { 'aria-label': 'Owner' },
        accessKey: "k",
        isValid: false,
        validationStatus: "pending",
        tabIndex: 1,
        stylingMode: "filled",
        text: "Hello",

        dataSource: makeAsyncDataSource('data.json'),

        contentTemplate: function (e) {
            var $list = $("<div>").dxList({

                searchEditorOptions: function () {
                    return new $("div").dxTextBox({
                        showClearButton: true,
                        placeholder: 'Enter state',
                        inputAttr: { 'aria-label': 'Search' },
                    })
                },
                searchExpr: ["state", "city"],
                searchTimeout: 500,
                searchEnabled: true,

                dataSource: e.component.getDataSource(),

                showSelectionControls: true,
                selectionMode: 'multiple',
                selectByClick: true,

                itemTemplate: function (itemData) {
                    return $("<div>").text(itemData.city);
                },
                onSelectionChanged(args) {
                    e.component.option("value", args.component.option('selectedItemKeys').join(', '));
                    console.log(e.component.option("value"));
                }
            });
            return $list;
        },

        dropDownButtonTemplate: function (icon, text) {
            icon = "add";
            text.prepend(`<span class="dx-icon-${icon}"></span>`);
        },

        onValueChanged(e) {
            valueChangedHandler(e);
            setTimeout(() => {
                console.log(multipleSelectionDropDownBox.getButton('clear'));
                console.log("Element : ", multipleSelectionDropDownBox.element());
                console.log("Content : ", multipleSelectionDropDownBox.content());
                console.log("Field : ", multipleSelectionDropDownBox.field());
                multipleSelectionDropDownBox.dispose();
            }, 5000);
        }
    }).dxDropDownBox("instance");

    // Gets the root UI component element.
    var multiSelectionElement = singleSelectionDropDownBox.element();

    // Gets the instance of a UI component found using its DOM node.
    var multiSelection = DevExpress.ui.dxDropDownBox.getInstance(multiSelectionElement);
    console.log(multiSelection.option("value"));

    setTimeout(() => {

        // Opens the drop-down editor.
        multipleSelectionDropDownBox.open();

        // Sets focus to the input element representing the UI component.
        multipleSelectionDropDownBox.focus();

    }, 3000);

    setTimeout(() => {

        // Closes the drop-down editor.
        multipleSelectionDropDownBox.close();

        // Removes focus from the input element.
        multipleSelectionDropDownBox.blur();

    }, 6000);

    // Subscribes to events.
    singleSelectionDropDownBox.on({
        "closed": closedHandler,
        "disposing": disposeHandler,
        "enterKey": enterKeyHandler,
        "focusIn": focusInHandler,
        "focusOut": focusOutHandler,
        "opened": openedHandler,
        "optionChanged": optionChangedHandler,

    });

    // Set default options for dxDropDownBox
    //const singleSelectionDropDownBox = $("#singleSelectionDropDownBox").dxDropDownBox({
    //    acceptCustomValue: true, // Defualt false
    //    accessKey: "A",
    //    activeStateEnabled: false,
    //    focusStateEnabled: false,
    //    value: "Patient",
    //    valueExpr: 'PATIENT_NAME',
    //    displayExpr: 'PATIENT_NAME',
    //    placeholder: 'Select a value...',
    //    showClearButton: true,
    //    dropDownOptions: {
    //        showTitle: true,
    //        title: 'Select patient',
    //        dragEnabled: true,
    //    },
    //    displayValueFormatter: function (value) {
    //        return "Patient : " + value;
    //    },
    //    hint: "Select or enter state",
    //    hoverStateEnabled: false,
    //    isValid: true,
    //    name: "Drop down",
    //    dataSource: getData("https://localhost:44343/api/CLRCD01/GetRecords"),
    //    contentTemplate: function (e) {
    //        $list = $("<div>").dxList({
    //            searchEditorOptions: {
    //                placeholder: 'Enter state',
    //                showClearButton: true,
    //                inputAttr: { 'aria-label': 'Search' },
    //            },
    //            searchExpr: ["PATIENT_NAME"],
    //            searchEnabled: true,
    //            dataSource: e.component.getDataSource(),
    //            showSelectionControls: true,
    //            selectionMode: 'single',
    //            itemTemplate: function (itemData) {
    //                return $("<div>").text(itemData.PATIENT_NAME);
    //            },
    //            //onSelectionChanged: function (args) {
    //            //    var selectedItems = args.addedItems[0];
    //            //    singleSelectionDropDownBox.option("value", selectedItems);
    //            //    singleSelectionDropDownBox.close();
    //            //}
    //            onItemClick: function (selectedItems) {
    //                singleSelectionDropDownBox.option("value", selectedItems.itemData.PATIENT_NAME);
    //                singleSelectionDropDownBox.close();
    //            }
    //        });
    //        return $list;
    //    },
    //    deferRendering:false,
    //}).dxDropDownBox("instance");

});
