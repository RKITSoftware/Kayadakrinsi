// Imports event functions from 'event.js'
import { changeHandler, closedHandler, copyHandler, cutHandler, contentReadyHandler, disposeHandler, enterKeyHandler, focusInHandler, focusOutHandler, initializedHandler, inputHandler, keyDownHandler, keyUpHandler, keyPressHandler, openedHandler, optionChangedHandler, pasteHandler, valueChangedHandler } from "../event.js";

import { states, products } from './Data.js';

$(function () {

    DevExpress.ui.dxSelectBox.defaultOptions({
        device: { deviceType: "desktop" },
        options: {
            height: 40,
            width: "50vw",
            hint: "Select your state.",
            //accessKey:"K",
        },
    });

    const fromUngroupedData = new DevExpress.data.DataSource({
        store: {
            type: 'array',
            data: products,
            key: 'ID',
        },
        group: 'Category',
    });


    const simpleSelectBox = $("#simpleSelectBox").dxSelectBox({
        items: states,
        inputAttr: {
            class: 'input-states'
        },
        elementAttr: {
            class: 'element-states'
        },
        acceptCustomValue: true,
        accessKey: "K",
        activeStateEnabled: false,
        focusStateEnabled: false,
        hoverStateEnabled: false,
        spellcheck: true,
        stylingMode: "filled",
        displayValue: states[6],
        maxLength: 10,
        name: "simpleSelectBox-name",
        text: "simpleSelectBox-text",
        showDropDownButton: false,
        //opened: true,
        openOnFieldClick: true,
        showClearButton: true,
        searchEnabled: true,
        searchMode: "startswith",
        showDataBeforeSearch: false,
        minSearchLength: 1,
        searchTimeout: 50,
    }).dxSelectBox("instance");


    const selectBox = $("#selectBox").dxSelectBox({
        items: states,
        tabIndex: 1,
        placeholder: "Select your state",
        value: states[0],
        buttons: [{
            name: "prevState",
            location: "before",
            options: {
                icon: "spinprev",
                onClick() {
                    const index = states.indexOf(selectBox.option("value"));
                    if (index != 0) {
                        selectBox.option("value", states[index - 1]);
                    }
                }
            }
        }, {
            name: "nextState",
            location: "after",
            options: {
                icon: "spinnext",
                onClick() {
                    const index = states.indexOf(selectBox.option("value"));
                    const length = states.length;
                    if (index != length - 1) {
                        selectBox.option("value", states[index + 1]);
                    }
                }
            }
        }, 'dropDown'],
        dropDownButtonTemplate: function (icon, text) {
            icon = "plus"; // The button's icon.
            text.prepend(`<span class="dx-icon-${icon}"></span>`); // The button's text.
        },
        dropDownOptions: {
            //maxHeight: "45vh",
            height: "30vh",
            width: "10vw",
        },
        wrapItemText: true,
        noDataText: "Something went wrong while loading data...",
    }).dxSelectBox("instance");


    const readOnlySelectBox = $("#readOnlySelectBox").dxSelectBox({
        items: states,
        //readOnly: true,
        //rtlEnabled: true,
        //visible:false,
        isValid: false,
        validationStatus: "valid",
        //validationMessageMode:"auto",
        //validationMessageMode:"always",
        // useItemTextAsTitle: true,
        itemTemplate: function (itemData) {
            return $("<div>").text(states.indexOf(itemData) + 1 + " " + itemData);
        },
    }).dxSelectBox("instance");


    const disabledSelectBox = $("#disabledSelectBox").dxSelectBox({
        items: states,
        disabled: true,
    }).dxSelectBox("instance");


    const dataSourceSelectBox = $("#dataSourceSelectBox").dxSelectBox({
        //dataSource: new DevExpress.data.ArrayStore({
        //    data: products,
        //    key: 'ID',
        //}),
        //dataSource: new DevExpress.data.DataSource({
        //    store: {
        //        type: 'array',
        //        data: products,
        //        key: 'ID',
        //    },
        //    group: 'Category',
        //}),
        dataSource: fromUngroupedData,
        grouped: true,
        groupTemplate(data) {
            return $(`<div class='custom-icon'><span class='dx-icon-home icon' style="color:aqua;"></span> ${data.key}</div>`);
        },
        displayExpr: 'Name',
        valueExpr: 'ID',
        value: products[0].ID,
        showSelectionControls: true,
        deferRendering: false,
        acceptCustomValue: true,
        searchEnabled: true,
        searchExpr: ["Price"],
        //searchMode: "startswith",
        searchTimeout: 0,
        onCustomItemCreating: function (data) {
            //customItemHandler(data);
            //e.customItem = null;
        },
        buttons: [{
            name: "Additem",
            location: "after",
            options: {
                icon: "plus",
                onClick: function (data) {
                    const productIds = products.map((item) => item.ID);
                    const incrementedId = Math.max.apply(null, productIds) + 1;
                    var val = dataSourceSelectBox.option("text");
                    const newItem = {
                        ID: incrementedId,
                        Name: val,
                        Price: 500,
                        Current_Inventory: 225,
                        Backorder: 0,
                        Manufacturing: 10,
                        Category: 'Video Players',
                    };
                    data.customItem = fromUngroupedData.store().insert(newItem)
                        .then(() => fromUngroupedData.load())
                        .then(() => newItem)
                        .catch((error) => {
                            throw error;
                        });
                }
            }
        }, 'dropDown'],
    }).dxSelectBox("instance");


    const customTemplateSelectBox = $("#customTemplateSelectBox").dxSelectBox({
        dataSource: products,
        displayExpr: 'Name',
        valueExpr: 'ID',
        value: products[3].ID,
        fieldTemplate: function (data, container) {
            var result = $(`<div class='custom-item'>
                                <div class='product-name'></div>
                                <div class='product-price'>Category : ${data ? data.Category : 0}</div>
                            </div>`);
            result
                .find('.product-name')
                .dxTextBox({
                    value: data && data.Name,
                    readOnly: true,
                });
            container.append(result);
        },
    }).dxSelectBox("instance");

    customTemplateSelectBox.beginUpdate();

    customTemplateSelectBox.focus();

    customTemplateSelectBox.blur();

    customTemplateSelectBox.endUpdate();


    const eventSelectBox = $("#eventSelectBox").dxSelectBox({
        onInitialized: function (e) {
            initializedHandler(e);
        }, onContentReady: function (e) {
            contentReadyHandler(e);
        },
    }).dxSelectBox("instance");

    eventSelectBox.option({
        acceptCustomValue: true,
        items: states,
        valueChangeEvent: "focusout",
        buttons: [{
            name: "disposeButton",
            location: "after",
            options: {
                icon: "trash",
                onClick: function () {
                    disposeHandler();
                    eventSelectBox.dispose();
                    setTimeout(() => {
                        eventSelectBox.repaint();
                    },3000);
                },
            }
        },'dropDown'],

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
            eventSelectBox.off("enterKey");
            //eventSelectBox.option("onPaste",undefined);
            //eventSelectBox.option("onPaste",0);
            //eventSelectBox.option("onPaste",false);
            eventSelectBox.option("onPaste",'');
            
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
            eventSelectBox.open();
        },

        // A function that is executed when a user presses a key on the keyboard.
        onKeyPress: function (e) {
            keyPressHandler(e);
        },

        // A function that is executed each time the UI component's input is changed while the UI component is focused.
        onInput: function (e) {
            inputHandler(e);
            eventSelectBox.close();
        },

        // A function that is executed after the UI component's value is changed.
        onValueChanged: function (e) {
            valueChangedHandler(e);
            console.log("Content : ", eventSelectBox.content());
            var element = eventSelectBox.element();
            console.log("Element : ", element);
            console.log("GetInstance : ", DevExpress.ui.dxSelectBox.getInstance(element));
            console.log("Instance : ", eventSelectBox.instance());
            console.log("Field : ", eventSelectBox.field());
            console.log("DataSource : ", eventSelectBox.getDataSource());
            console.log("Button : ", eventSelectBox.getButton("disposeButton"));
        },

        onItemClick: function (item) {
            console.log("Item clicked", item.itemData);
        },

        onSelectionChanged: function (e) {
            console.log("Selection changed to", e.selectedItem);
        },

    });
    
    eventSelectBox.on({
        "closed": closedHandler,
        "disposing": disposeHandler,
        "enterKey": enterKeyHandler,
        "focusIn": focusInHandler,
        "focusOut": focusOutHandler,
        "opened": openedHandler,
        "optionChanged": optionChangedHandler,
        //"krinsi": function () { alert("CUT 2.0") },
        //"cut": function () { alert("CUT 2.0") },
        //"changed": changeHandler,
        //"input": inputHandler,
        //"valueChanged":valueChangedHandler,
    });   

});






























//function customItemHandler(data) {
    //    if (!data.text) {
    //        data.customItem = null;
    //        return;
    //    }

    //    console.log(data.component.getDataSource().items());
    //    let existingItem = data.component.getDataSource().items().filter(product => product.name === data.text)?.[0];

    //    if (existingItem != undefined) {
    //        dataSourceSelectBox.option("value", existingItem?.id);
    //        data.customItem = (new Promise(function () { })).then(() => existingItem);
    //        console.log("ok");
    //        return;
    //    }

    //    const productIds = products.map((item) => item.ID);
    //    const incrementedId = Math.max.apply(null, productIds) + 1;
    //    const newItem = {
    //        ID: incrementedId,
    //        Name: data.text,
    //        Price: 500,
    //        Current_Inventory: 225,
    //        Backorder: 0,
    //        Manufacturing: 10,
    //        Category: 'Video Players',
    //    };

    //    data.customItem = fromUngroupedData.store().insert(newItem)
    //        .then(() => fromUngroupedData.load())
    //        .then(() => newItem)
    //        .catch((error) => {
    //            throw error;
    //        });
    //}




















