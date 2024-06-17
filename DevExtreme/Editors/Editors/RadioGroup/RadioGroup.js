import { Gender, Shapes } from "./Data.js";
import { contentReadyHandler, disposeHandler, initializedHandler, optionChangedHandler, valueChangedHandler } from "../event.js";

$(() => {

    DevExpress.ui.dxRadioGroup.defaultOptions({
        device: { deviceType: "desktop" },
        options: {
            element: {
                'area-label': "RadioGroup-element",
            },
            name: "RadioGroup-name",
            hint: "Choose at most one option",
            accessKey: "R",
            //height: 30,
            //width:30,
        }
    });


    const simpleRadioGroup = $("#simpleRadioGroup").dxRadioGroup({
        items: Gender,
        value: Gender[1],
    }).dxRadioGroup("instance");


    const horizontalRadioGroup = $("#horizontalRadioGroup").dxRadioGroup({
        items: Gender,
        layout: 'horizontal',
        tabIndex: 1,
        activeStateEnabled: false,
        focusStateEnabled: false,
        hoverStateEnabled: false,
    }).dxRadioGroup("instance");


    const dataSourceRadioGroup = $("#dataSourceRadioGroup").dxRadioGroup({
        dataSource: Shapes,
        //displayExpr: "Id",
        displayExpr: "Name",
        valueExpr: "Id",
        itemTemplate: function (itemData) {
            return $("<div>").addClass(`dx-icon-add`).text(itemData.Name);
        },
    }).dxRadioGroup("instance");


    const eventsRadioGroup = $("#eventsRadioGroup").dxRadioGroup({
        onInitialized: initializedHandler,
        onContentReady: function (e) {
            contentReadyHandler(e);
        },
        items: Gender,
    }).dxRadioGroup("instance");

    eventsRadioGroup.on({
        "optionChanged": optionChangedHandler,
        "disposing":
            disposeHandler,
        "valueChanged": function (e) {
            valueChangedHandler(e);
            var element = eventsRadioGroup.element();
            console.log("Element : ", element);
            console.log("GetInstance : ", DevExpress.ui.dxRadioGroup.getInstance(element));
            console.log("GetDataSource : ", eventsRadioGroup.getDataSource());
            console.log("Instance : ", eventsRadioGroup.instance());
            eventsRadioGroup.dispose();
            //setTimeout(function () {
            //    console.log("Hello from timeout");
            //    eventsRadioGroup.repaint();
            //}, 3000);
        },
    })


    const readOnlyRadioGroup = $("#readOnlyRadioGroup").dxRadioGroup({
        items: Gender,
        readOnly: true,
    }).dxRadioGroup("instance");


    const disabledRadioGroup = $("#disabledRadioGroup").dxRadioGroup({
        items: Gender,
        disabled: true,
        rtlEnabled: true,
        isValid: false,
        validationStatus: "pending",
        //visible:true,
    }).dxRadioGroup("instance");


});